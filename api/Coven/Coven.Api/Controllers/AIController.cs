using Coven.Api.Services;
using Coven.Data.AI;
using Coven.Data.DTO.AI;
using Coven.Data.Pinecone;
using Coven.Data.Repository;
using Coven.Logic.Base_Types;
using Coven.Logic.DTO.WorldAnvil;
using Coven.Logic.Meta_Objects;
using Coven.Logic.Request_Models.get;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Embedding;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Coven.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIController : ControllerBase
    {
        private readonly OpenAIAPI OpenAIClient;
        private readonly IConfiguration _config;

        private readonly IRepository Repository;
        private readonly IWorldAnvilService WorldAnvilService;
        private readonly IPineconeService PineconeService;

        public AIController(IConfiguration config,
            IWorldAnvilService _worldAnvilService,
            IRepository _repository,
            IPineconeService _pinecone)
        {
            _config = config;
            OpenAIClient = new OpenAIAPI(_config["OpenAIAPIKey"]);

            Repository = _repository;
            WorldAnvilService = _worldAnvilService;
            PineconeService = _pinecone;
        }

        /// <summary>
        /// Adds the embeddings for all WA articles in a world to Pinecone
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="worldId"></param>
        /// <returns></returns>
        [HttpPost("AddArticleEmbeddings")]
        public async Task<ActionResult> AddArticleEmbeddings(Guid userId, Guid worldId)
        {
            List<ArticleMeta> worldArticleMetaList = await WorldAnvilService.GetArticleMetas(worldId);
            WorldSegment worldSegment = await WorldAnvilService.GetWorld(worldId);

            ConcurrentBag<Embedding> embeddingsBag = new ConcurrentBag<Embedding>();
            ConcurrentBag<EmbedReport> articleReportBag = new ConcurrentBag<EmbedReport>();

            // Run tasks in parallel to process articles
            var articleProcessingTasks = worldArticleMetaList.Select(async meta =>
            {
                Article article = await WorldAnvilService.GetArticle(meta.id);
                if (string.IsNullOrEmpty(article.content))
                {
                    articleReportBag.Add(new EmbedReport()
                    {
                        success = false,
                        identifier = article.title,
                        message = "Article has no content"
                    });
                    return;
                }
                List<SentenceVectorDTO> articleVectors = await PineconeService.GetVectorsFromArticle(article);

                foreach (SentenceVectorDTO entry in articleVectors)
                {
                    embeddingsBag.Add(new Embedding()
                    {
                        identifier = Guid.NewGuid().ToString(),
                        vectors = entry.Vector.ToArray(),
                        metadata = new ArticleMetadata()
                        {
                            WorldId = worldId.ToString(),
                            ArticleId = meta.id.ToString(),
                            CharacterString = entry.Sentence
                        }
                    });
                }
            });

            try
            {
                await Task.WhenAll(articleProcessingTasks);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            

            List<Embedding> ExceededSizeEmbeddings = embeddingsBag.Where(x => x.vectors.Length > 1536).ToList();
            embeddingsBag = new ConcurrentBag<Embedding>(embeddingsBag.Where(x => !ExceededSizeEmbeddings.Contains(x)));
            foreach (var e in ExceededSizeEmbeddings.Select(e => new EmbedReport()
            {
                identifier = e.identifier,
                success = false,
                message = "Embedding size exceeded 1536"
            }))
            {
                articleReportBag.Add(e);
            }

            List<EmbedReport> articleReportList = articleReportBag.ToList();
            List<Embedding> embeddingsList = embeddingsBag.ToList();

            while (embeddingsList.Count > 0)
            {
                // Pinecone has a limit of 250 vectors per request
                int batchSize = Math.Min(250, embeddingsList.Count);

                // Create a separate list for the current batch
                List<Embedding> batch = embeddingsList.Take(batchSize).ToList();

                // Upload the batch
                bool success = await PineconeService.UpsertVectors(worldSegment.name, batch);

                // Take any batches that failed and add them to the failed report
                if (!success)
                {
                    articleReportList.AddRange(batch.Select(e => new EmbedReport()
                    {
                        identifier = e.identifier,
                        success = false,
                        message = "Failed to upload embedding"
                    }));
                }
                // Else any successful ones get marked as successful
                else
                {
#warning Could make a failsafe here to delete the entries if the database update fails
                    await Repository.CreatePineconeMetadataEntries(userId, batch);

                    articleReportList.AddRange(batch.Select(e => new EmbedReport()
                    {
                        identifier = e.identifier,
                        success = true,
                        message = "Embedding uploaded successfully"
                    }));
                }

                embeddingsList.RemoveRange(0, batchSize);
            }

            return Ok(new
            {
                succeededOperationsCount = articleReportList.Where(x => x.success).Count(),
                failedOperationsCount = articleReportList.Where(x => !x.success).Count(),
                succeededOperations = articleReportList.Where(x => x.success),
                failedOperations = articleReportList.Where(x => !x.success),
                totalReport = articleReportList
            });
        }
    }

    public class EmbedReport
    {
        public string identifier { get; set; }
        public string message { get; set; }
        public bool success { get; set; }
    }

}

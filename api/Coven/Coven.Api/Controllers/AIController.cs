using Coven.Api.Services;
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

        [HttpPost("AddArticleEmbeddings")]
        public async Task<ActionResult> AddArticleEmbeddings(Guid userId, Guid worldId)
        {
            List<ArticleMeta> worldArticleMetaList = await WorldAnvilService.GetArticleMetas(worldId);
            WorldSegment worldSegment = await WorldAnvilService.GetWorld(worldId);
            List<Embedding> embeddings = new List<Embedding>();

            // Foreach article, get the embedding and add it to the list
            foreach (ArticleMeta meta in worldArticleMetaList)
            {
                Article article = await WorldAnvilService.GetArticle(meta.id);
                List<float> articleVectors = await PineconeService.GetVectorsFromArticle(article);

                embeddings.Add(new Embedding()
                {
                    identifier = article.title,
                    vectors = articleVectors.ToArray(),
                    metadata = new ArticleMetadata()
                    {
                        worldId = worldId.ToString(),
                        articleId = meta.id.ToString(),
                        Title = meta.title,
                        ArticleType = meta.templateType,
                        Author = meta.author.username,
                    }
                });
            }
            while(embeddings.Count > 0)
            {
                // Pinecone has a limit of 250 vectors per request
                int batchSize = Math.Min(250, embeddings.Count);

                foreach (var embedding in embeddings.Take(batchSize))
                {
                    await PineconeService.UpsertVectors(worldSegment.name, embeddings);
                }

                embeddings.RemoveRange(0, batchSize);
            }

            return Ok();
        }
    }
}

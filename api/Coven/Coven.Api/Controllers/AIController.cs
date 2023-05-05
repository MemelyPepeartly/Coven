using Coven.Api.Services;
using Coven.Data.DTO.AI;
using Coven.Data.Repository;
using Coven.Logic.DTO.WorldAnvil;
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
        private readonly OpenAIAPI _openAIAPI;
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
            _openAIAPI = new OpenAIAPI(_config["OPENAI_API_KEY"]);

            Repository = _repository;
            WorldAnvilService = _worldAnvilService;
            PineconeService = _pinecone;
        }


        [HttpPost("AddEmbeddings")]
        public async Task<ActionResult> AddEmbeddings(Guid userId, Guid worldId)
        {
            WorldArticlesSummary world = await WorldAnvilService.GetWorldArticlesSummary(worldId);
            List<string> articleTitles = world.articles
                .Select(s => s.title)
                .ToList();

            List<Embedding> embeddings = new List<Embedding>();
            foreach (string title in articleTitles)
            {
                EmbeddingResult embedding = await _openAIAPI.Embeddings.CreateEmbeddingAsync(title);
                embeddings.Add(new Embedding()
                {
                    characterSet = title,
                    vectors = embedding.Data.SelectMany(v => v.Embedding).ToArray()
                });
            }
            while(embeddings.Count > 0)
            {
                // Pinecone has a limit of 250 vectors per request
                int batchSize = Math.Min(250, embeddings.Count);

                foreach (var embedding in embeddings.Take(batchSize))
                {
                    await PineconeService.UpsertVectors(world.world.title, embeddings);
                }

                embeddings.RemoveRange(0, batchSize);
            }

            return Ok();
        }
    }
}

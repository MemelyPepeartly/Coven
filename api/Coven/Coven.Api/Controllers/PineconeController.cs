using Coven.Api.Services;
using Coven.Data.Repository;
using Coven.Logic.Base_Types;
using Coven.Logic.Request_Models.Post;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API;

namespace Coven.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PineconeController : ControllerBase
    {
        private readonly OpenAIAPI _openAIAPI;
        private readonly IConfiguration _config;
        private readonly IPineconeService PineconeService;
        private readonly IWorldAnvilService WorldAnvilService;
        private readonly IRepository Repository;

        public PineconeController(IConfiguration config,
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

        [HttpPost("Query")]
        public async Task<ActionResult> GetIndexes([FromBody] CreatePineconeQueryModel model)
        {
            var queryVectors = (await _openAIAPI.Embeddings.CreateEmbeddingAsync(model.query)).Data
                .SelectMany(v => v.Embedding)
                .ToArray();

            var world = await WorldAnvilService.GetWorldArticlesSummary(model.worldId);

            return Ok(await PineconeService.QueryPineconeIndex(world.world.title, model.query, queryVectors));
        }
    }
}

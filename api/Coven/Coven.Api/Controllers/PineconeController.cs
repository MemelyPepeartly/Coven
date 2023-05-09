using Coven.Api.Services;
using Coven.Data.AI;
using Coven.Data.DTO.AI;
using Coven.Data.Pinecone;
using Coven.Data.Repository;
using Coven.Logic.Base_Types;
using Coven.Logic.DTO.WorldAnvil;
using Coven.Logic.Meta_Objects;
using Coven.Logic.Request_Models.Post;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using System.Text.RegularExpressions;

namespace Coven.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PineconeController : ControllerBase
    {
        private readonly OpenAIAPI OpenAIClient;
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
            OpenAIClient = new OpenAIAPI(_config["OpenAIAPIKey"]);

            Repository = _repository;
            WorldAnvilService = _worldAnvilService;
            PineconeService = _pinecone;
        }

        [HttpPost("Query")]
        public async Task<ActionResult> GetIndexes([FromBody] CreatePineconeQueryModel model)
        {
            var queryVectors = (await OpenAIClient.Embeddings.CreateEmbeddingAsync(model.query)).Data
                .SelectMany(v => v.Embedding)
                .ToArray();

            var world = await WorldAnvilService.GetWorldArticlesSummary(model.worldId);

            return Ok(await PineconeService.QueryPineconeIndex(world.world.title, model.query, queryVectors));
        }

        [HttpPost("AddArticleSnippet")]
        public async Task<ActionResult> AddArticleSnippet([FromBody] CreateVectorsFromArticleModel model)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("RemoveWorldVectors")]
        public async Task<ActionResult> RemoveWorldVectors(Guid worldId)
        {
            WorldArticlesSummary world = await WorldAnvilService.GetWorldArticlesSummary(worldId);

            return Ok(await PineconeService.DeleteAllVectorsFromNamespace(world.world.title));
        }
    }
}

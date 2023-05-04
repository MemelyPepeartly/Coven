using Coven.Api.Services;
using Coven.Logic.DTO.AI;
using Coven.Logic.Request_Models.get;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API;

namespace Coven.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIController : ControllerBase
    {
        private readonly OpenAIAPI _openAIAPI;
        private readonly IConfiguration _config;

        private readonly IWorldAnvilService WorldAnvilService;

        public AIController(IConfiguration config, IWorldAnvilService _worldAnvilService)
        {
            _config = config;
            _openAIAPI = new OpenAIAPI(_config["OPENAI_API_KEY"]);

            WorldAnvilService = _worldAnvilService;
        }

        [HttpPost("GetArticleEmbeddings")]
        public async Task<ActionResult> GetArticleEmbeddings([FromBody]GetEmbeddingsRequestModel model)
        {
            EmbeddingsDTO DTO = new EmbeddingsDTO();
            foreach (string word in model.userInput.Split(" "))
            {
                var thing = await _openAIAPI.Embeddings.CreateEmbeddingAsync(word);

                DTO.queryEmbeddings.Add(new Embedding()
                {
                    characterSet = word,
                    vector = (await _openAIAPI.Embeddings.CreateEmbeddingAsync(word)).Data
                }) ;
            }
            

            List<string> existingEmbeddings = (await WorldAnvilService.GetWorlds()).worlds
                .Select(s => s.name)
                .ToList();

            foreach (string thing in existingEmbeddings)
            {
                DTO.queryEmbeddings.Add(new Embedding()
                {
                    characterSet = thing,
                    vector = (await _openAIAPI.Embeddings.CreateEmbeddingAsync(thing)).Data
                });
            }

            var result = CompareEmbeddings(DTO);

            return Ok(result);
        }

        private object CompareEmbeddings(object DTO)
        {
            return true;
        }
    }
}

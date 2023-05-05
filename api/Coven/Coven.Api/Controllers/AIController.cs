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

        public AIController(IConfiguration config, IWorldAnvilService _worldAnvilService, IRepository _repository)
        {
            _config = config;
            _openAIAPI = new OpenAIAPI(_config["OPENAI_API_KEY"]);

            Repository = _repository;
            WorldAnvilService = _worldAnvilService;
        }

        /// <summary>
        /// Ideally used for sending in a query about a world or concept and getting back a list of worlds that are similar to the query.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("QueryWorlds")]
        public async Task<ActionResult> QueryWorlds([FromBody]GetEmbeddingsRequestModel model)
        {
            EmbeddingsDTO DTO = new EmbeddingsDTO();
            foreach (string word in model.userInput.Split(" "))
            {
                var thing = await _openAIAPI.Embeddings.CreateEmbeddingAsync(word);

                DTO.queryEmbeddings.Add(new Embedding()
                {
                    characterSet = word,
                    vectors = (await _openAIAPI.Embeddings.CreateEmbeddingAsync(word)).Data.SelectMany(v => v.Embedding).ToArray()
                });
            }

            var worlds = await WorldAnvilService.GetWorlds();
            List<string> articleTitles = (await WorldAnvilService.GetWorldArticlesSummary(worlds.worlds[1].id)).articles
                .Select(s => s.title)
                .ToList();

            List<WACharacterSetDTO> existingEmbeddings = await Repository.GetUserEmbeddings(model.userId);

            foreach (var embed in existingEmbeddings)
            {
                DTO.worldEmbeddings.Add(new Embedding()
                {
                    characterSet = embed.characterSet,
                    vectors = embed.vectors
                });
            }

            var result = GetRelatedEmbeddings(DTO);

            return Ok(result);
        }

        [HttpPost("AddEmbeddings")]
        public async Task<ActionResult> AddEmbeddings(Guid userId, Guid worldId)
        {
            WorldsSummary worlds = await WorldAnvilService.GetWorlds();
            List<string> articleTitles = (await WorldAnvilService.GetWorldArticlesSummary(worldId)).articles
                .Select(s => s.title)
                .ToList();
            foreach (string title in articleTitles)
            {
                EmbeddingResult embedding = await _openAIAPI.Embeddings.CreateEmbeddingAsync(title);

                var vectors = embedding.Data
                    .SelectMany(t => t.Embedding)
                    .ToArray();
                await Repository.CreateEmbeddings(userId, title, vectors);
            }
            return Ok(await Repository.GetUserEmbeddings(userId));
        }

        // Calculate the dot product of two vectors
        private static float DotProduct(float[] vecA, float[] vecB)
        {
            float dotProduct = 0;
            for (int i = 0; i < vecA.Length; i++)
            {
                dotProduct += vecA[i] * vecB[i];
            }
            return dotProduct;
        }

        // Calculate the magnitude of a vector
        private static float Magnitude(float[] vec)
        {
            float sumOfSquares = 0;
            for (int i = 0; i < vec.Length; i++)
            {
                sumOfSquares += vec[i] * vec[i];
            }
            return (float)Math.Sqrt(sumOfSquares);
        }

        // Calculate the cosine similarity between two vectors
        private static float CalculateCosineSimilarity(float[] vecA, float[] vecB)
        {
            float dotProduct = DotProduct(vecA, vecB);
            float magnitudeA = Magnitude(vecA);
            float magnitudeB = Magnitude(vecB);
            return dotProduct / (magnitudeA * magnitudeB);
        }

        // Get related embeddings using cosine similarity
        public static List<Embedding> GetRelatedEmbeddings(EmbeddingsDTO embeddingsDTO, float similarityThreshold = 0.7f)
        {
            List<Embedding> relatedEmbeddings = new List<Embedding>();

            foreach (Embedding queryEmbedding in embeddingsDTO.queryEmbeddings)
            {
                foreach (Embedding worldEmbedding in embeddingsDTO.worldEmbeddings)
                {
                    float similarity = CalculateCosineSimilarity(queryEmbedding.vectors, worldEmbedding.vectors);
                    if (similarity >= similarityThreshold)
                    {
                        relatedEmbeddings.Add(worldEmbedding);
                    }
                }
            }

            return relatedEmbeddings;
        }

    }
}

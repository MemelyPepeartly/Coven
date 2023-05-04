using Coven.Api.Services;
using Coven.Logic.DTO.AI;
using Coven.Logic.Request_Models.get;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using System.Diagnostics;

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
                    vector = (await _openAIAPI.Embeddings.CreateEmbeddingAsync(word)).Data
                });
            }

            var worlds = await WorldAnvilService.GetWorlds();
            List<string> articleTitles = (await WorldAnvilService.GetWorldArticlesSummary(worlds.worlds[1].id)).articles
                .Select(s => s.title)
                .ToList();

            foreach (string thing in articleTitles)
            {
                DTO.worldEmbeddings.Add(new Embedding()
                {
                    characterSet = thing,
                    vector = (await _openAIAPI.Embeddings.CreateEmbeddingAsync(thing)).Data
                });
            }

            var result = GetRelatedEmbeddings(DTO);

            return Ok(result);
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
                    float similarity = CalculateCosineSimilarity(queryEmbedding.vector.SelectMany(x => x.Embedding).ToArray(), worldEmbedding.vector.SelectMany(x => x.Embedding).ToArray());
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

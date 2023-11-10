using Coven.Data.AI;
using Coven.Data.DTO.AI;
using Coven.Data.Entities;
using Coven.Data.Pinecone;
using Coven.Logic.Base_Types;
using Coven.Logic.Request_Models.Post;
using RestSharp;

namespace Coven.Api.Services
{
    public interface IPineconeService
    {
        /// <summary>
        /// Upserts a vector into a Pinecone index using worldtitle as a namespace, characterSet as the string of characters the vectors are for, and a float index of vectors.
        /// </summary>
        /// <param name="worldTitle"></param>
        /// <param name="vectorId"></param>
        /// <param name="vectors"></param>
        /// <returns></returns>
        Task<bool> UpsertVectors(string worldTitle, string vectorId, float[] vectors, PineconeMetadata customMetadata);
        Task<bool> UpsertVectors(string worldTitle, Embedding embedding);
        Task<bool> UpsertVectors(string worldTitle, List<Embedding> embeddings);
        Task<PineconeQueryResponse> QueryPineconeIndex(string worldTitle, string query, float[] queryVectors);
        Task<float[]> GetVectors(string identifier);

        Task<bool> DeleteAllVectorsFromNamespace(string worldTitle);

        #region utility
        /// <summary>
        /// Removes BBCode tags from a string.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string RemoveBBCodeAndExtraSpaces(string input);
        /// <summary>
        /// Takes a large string input and splits it into sentences. The sentences will not exceed the max length for tokens using the OpenAI API.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<string> SplitStringIntoSentences(string input);
        /// <summary>
        /// Gets a list of vectors from a potentially large string by splitting it into chunks and getting the embeddings for each chunk.
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        Task<List<SentenceVectorDTO>> GetVectorsFromArticle(Article article);
        #endregion
    }
}

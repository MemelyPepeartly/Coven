using Coven.Data.DTO.AI;
using Coven.Data.Entities;
using Coven.Logic.Request_Models.Post;
using RestSharp;

namespace Coven.Api.Services
{
    public interface IPineconeService
    {
        /// <summary>
        /// Upserts a vector into a Pinecone index using worldtitle as a namespace, characterSet as the string of characters the vectors are for, and a float index of vectors.
        /// </summary>
        /// <param name="worldtitle"></param>
        /// <param name="characterSet"></param>
        /// <param name="vectors"></param>
        /// <returns></returns>
        Task<RestResponse> UpsertVectors(string worldtitle, string characterSet, float[] vectors);
        Task<RestResponse> UpsertVectors(string worldTitle, List<Embedding> embeddings);
        Task<PineconeQueryResponse> QueryPineconeIndex(string worldTitle, string query, float[] queryVectors);
        Task<float[]> GetVectors(string identifier);
    }
}

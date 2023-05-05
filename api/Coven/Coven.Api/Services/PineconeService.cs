using Azure.Core;
using Coven.Data.DTO.AI;
using Coven.Data.Entities;
using Coven.Data.Pinecone;
using Coven.Logic.Request_Models.Post;
using Newtonsoft.Json;
using RestSharp;

namespace Coven.Api.Services
{
    public class PineconeService : IPineconeService
    {
        private IRestClient RestClient;
        private readonly IConfiguration _config;


        public PineconeService(IConfiguration config)
        {
            _config = config;

            RestClient = new RestClient("https://coven-qa-820470b.svc.us-east-1-aws.pinecone.io/");
        }

        public Task<float[]> GetVectors(string identifier)
        {
            throw new NotImplementedException();
        }

        public async Task<RestResponse> UpsertVectors(string worldTitle, string characterSet, float[] vectors)
        {
            var request = CreateRequest("vectors/upsert", Method.Post);
            request.AddJsonBody<UpsertRequest>(new UpsertRequest()
            {
                Namespace = worldTitle,
                Vectors = new List<Vector>()
                {
                    new Vector()
                    {
                        Id = characterSet,
                        Values = vectors.ToList(),
                        Metadata = new Metadata()
                        {
                            Genre = "Fantasy"
                        }
                    }
                }
            });

            RestResponse response = await RestClient.ExecuteAsync(request);

            return response;
        }

        public async Task<RestResponse> UpsertVectors(string worldTitle, List<Embedding> embeddings)
        {
            var request = CreateRequest("upsert", Method.Post);
            request.AddJsonBody<UpsertRequest>(new UpsertRequest()
            {
                Namespace = worldTitle,
                Vectors = embeddings.Select(e => new Vector()
                {
                    Id = e.characterSet,
                    Values = e.vectors.ToList(),
                    Metadata = new Metadata()
                    {
                        Genre = "Fantasy"
                    }
                }).ToList()
            });

            RestResponse response = await RestClient.ExecuteAsync(request);

            return response;
        }

        private RestRequest CreateRequest(string resource, Method method)
        {
            var request = new RestRequest(resource, method);
            request.AddHeader("accept", "application/json");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("Api-Key", _config["PineConeApiKey"]);
            return request;
        }

        public async Task<PineconeQueryResponse> QueryPineconeIndex(string worldTitle, string query, float[] queryVectors)
        {
            var request = CreateRequest("query", Method.Post);
            request.AddJsonBody<PineconeQueryRequest>(new PineconeQueryRequest()
            {
                Namespace = worldTitle,
                Vector = queryVectors.ToList(),
                TopK = 10,
                IncludeMetadata = true,
                IncludeValues = false
            });

            RestResponse response = await RestClient.ExecuteAsync(request);
            var result = JsonConvert.DeserializeObject<PineconeQueryResponse>(response.Content);

            return result != null ? result : new PineconeQueryResponse();
        }
    }
}

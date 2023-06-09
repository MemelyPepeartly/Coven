﻿using Azure.Core;
using Coven.Data.AI;
using Coven.Data.DTO.AI;
using Coven.Data.Entities;
using Coven.Data.Pinecone;
using Coven.Logic.Base_Types;
using Coven.Logic.Request_Models.Post;
using Newtonsoft.Json;
using OpenAI_API;
using OpenAI_API.Embedding;
using RestSharp;
using System.Text.RegularExpressions;

namespace Coven.Api.Services
{
    public class PineconeService : IPineconeService
    {
        private readonly OpenAIAPI OpenAIClient;

        private IRestClient RestClient;
        private readonly IConfiguration _config;

        public PineconeService(IConfiguration config)
        {
            _config = config;
            OpenAIClient = new OpenAIAPI(_config["OpenAIAPIKey"]);
            RestClient = new RestClient($"https://{_config["PineconeAppUri"]}");
        }

        public Task<float[]> GetVectors(string identifier)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpsertVectors(string worldTitle, string vectorId, float[] vectors, PineconeMetadata customMetadata = null)
        {
            var request = CreateRequest("vectors/upsert", Method.Post);
            PineconeMetadata metadata = customMetadata;
            if (customMetadata == null)
            {
                metadata = new DefaultMetadata()
                {
                    Summary = $"{worldTitle} {vectorId}"
                };
            }
            else
            {
                metadata = customMetadata;
            }

            request.AddJsonBody<UpsertRequest>(new UpsertRequest()
            {
                Namespace = worldTitle,
                Vectors = new List<Vector>()
                {
                    new Vector()
                    {
                        Id = vectorId,
                        Values = vectors.ToList(),
                        Metadata = metadata
                    }
                }
            });

            RestResponse response = await RestClient.ExecuteAsync(request);

            return response.IsSuccessful;
        }

        public async Task<bool> UpsertVectors(string worldTitle, Embedding embedding)
        {
            var request = CreateRequest("vectors/upsert", Method.Post);
            request.AddJsonBody<UpsertRequest>(new UpsertRequest()
            {
                Namespace = worldTitle,
                Vectors = new List<Vector>()
                {
                    new Vector()
                    {
                        Id = embedding.identifier,
                        Values = embedding.vectors.ToList(),
                        Metadata = embedding.metadata
                    }
                }
            });

            RestResponse response = await RestClient.ExecuteAsync(request);

            return response.IsSuccessful;
        }

        public async Task<bool> UpsertVectors(string worldTitle, List<Embedding> embeddings)
        {
            var request = CreateRequest("vectors/upsert", Method.Post);
            request.AddJsonBody<UpsertRequest>(new UpsertRequest()
            {
                Namespace = worldTitle,
                Vectors = embeddings.Select(e => new Vector()
                {
                    Id = e.identifier,
                    Values = e.vectors.ToList(),
                    Metadata = e.metadata
                }).ToList()
            });

            RestResponse response = await RestClient.ExecuteAsync(request);

            return response.IsSuccessful;
        }

        public async Task<PineconeQueryResponse> QueryPineconeIndex(string worldTitle, string query, float[] queryVectors)
        {
            var request = CreateRequest("query", Method.Post);
            request.AddJsonBody<PineconeQueryRequest>(new PineconeQueryRequest()
            {
                Namespace = worldTitle,
                Vector = queryVectors.ToList(),
                TopK = 25,
                IncludeMetadata = true,
                IncludeValues = false
            });

            RestResponse response = await RestClient.ExecuteAsync(request);
            var result = JsonConvert.DeserializeObject<PineconeQueryResponse>(response.Content);

            return result != null ? result : new PineconeQueryResponse();
        }

        public async Task<bool> DeleteAllVectorsFromNamespace(string worldTitle)
        {
            var request = CreateRequest("vectors/delete", Method.Post);
            request.AddJsonBody<object>(new
            {
                @namespace = worldTitle,
                deleteAll = true
            });
            RestResponse response = await RestClient.ExecuteAsync(request);
            return response.IsSuccessful;
        }

        private RestRequest CreateRequest(string resource, Method method)
        {
            var request = new RestRequest(resource, method);
            request.AddHeader("accept", "application/json");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("Api-Key", _config["PineConeApiKey"]);
            return request;
        }

        #region utility
        public string RemoveBBCodeAndExtraSpaces(string input)
        {
            // Remove BBCode
            var bbCodeRegex = new Regex(@"\[(\w+)(?:[^\]])*](.*?)(\[/\1]|\[/\w+])", RegexOptions.Singleline);
            var stringWithoutBBCode = bbCodeRegex.Replace(input, string.Empty);

            // Remove single BBCode tags (without closing tags) and self-closing tags
            var singleTagRegex = new Regex(@"\[(\w+)(?:[^\]])*](?!.*\[/\1])|\[.*?/]", RegexOptions.Singleline);
            stringWithoutBBCode = singleTagRegex.Replace(stringWithoutBBCode, string.Empty);

            // Replace multiple spaces with a single space
            var multipleSpacesRegex = new Regex(@" {2,}", RegexOptions.Singleline);
            stringWithoutBBCode = multipleSpacesRegex.Replace(stringWithoutBBCode, " ");

            // Remove multiple newline characters
            var newLineRegex = new Regex(@"(\r?\n){2,}", RegexOptions.Singleline);
            stringWithoutBBCode = newLineRegex.Replace(stringWithoutBBCode, "\n");

            // Remove leading and trailing whitespaces
            stringWithoutBBCode = stringWithoutBBCode.Trim();

            return stringWithoutBBCode;
        }

        public List<string> SplitStringIntoSentences(string input)
        {
            var sentences = new List<string>();
            var sentenceBoundaries = new Regex(@"(?<=[\.!\?])\s+(?=[A-Z])");
            var tokenLimit = 4097;

            // Split the input string into sentences based on the sentence boundaries.
            var sentenceList = sentenceBoundaries.Split(input).ToList();

            foreach (var sentence in sentenceList)
            {
                // Calculate the number of tokens in the current sentence.
                int tokens = (int)Math.Ceiling(sentence.Length / 3.0);

                if (tokens <= tokenLimit)
                {
                    sentences.Add(sentence);
                }
                else
                {
                    // If the sentence exceeds the token limit, truncate it.
                    int charsToKeep = tokenLimit * 3;
                    string truncatedSentence = sentence.Substring(0, charsToKeep);
                    sentences.Add(truncatedSentence);
                }
            }

            return sentences;
        }

        public async Task<List<SentenceVectorDTO>> GetVectorsFromArticle(Article article)
        {
            List<string> articleSections = SplitStringIntoSentences(RemoveBBCodeAndExtraSpaces(article.content));

            List<SentenceVectorDTO> sentenceVectors = new List<SentenceVectorDTO>();

            foreach (var sentence in articleSections)
            {
                if(sentence.Length < 3)
                {
                    continue;
                }

                var vector = (await OpenAIClient.Embeddings.CreateEmbeddingAsync(sentence)).Data.SelectMany(v => v.Embedding).ToList();
                sentenceVectors.Add(new SentenceVectorDTO { Sentence = sentence, Vector = vector });
            }
            return sentenceVectors;
        }
        #endregion
    }
}

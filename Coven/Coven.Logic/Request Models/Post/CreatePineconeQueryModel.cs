using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coven.Logic.Request_Models.Post
{
    public class CreatePineconeQueryModel
    {
        public Guid worldId { get; set; }
        public string query { get; set; }
    }

    public class PineconeQueryResponse
    {
        [JsonProperty("results")]
        public List<Match> Results { get; set; }

        [JsonProperty("matches")]
        public List<Match> Matches { get; set; }

        [JsonProperty("namespace")]
        public string Namespace { get; set; }
    }

    public class Match
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("score")]
        public double Score { get; set; }

        [JsonProperty("values")]
        public List<float> Values { get; set; }

        [JsonProperty("metadata")]
        public QueryResponseMetadata Metadata { get; set; }
    }

    public class QueryResponseMetadata
    {
        [JsonProperty("genre")]
        public string Genre { get; set; }
    }
}

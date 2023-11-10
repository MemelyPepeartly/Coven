using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Coven.Data.Pinecone
{
    public class UpsertRequest
    {
        [JsonProperty("vectors")]
        public List<Vector> Vectors { get; set; }
        /// <summary>
        /// The name of the associated dataset
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }
    }
    public class Vector
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("values")]
        public List<float> Values { get; set; }
        [JsonProperty("metadata")]
        public PineconeMetadata Metadata { get; set; }
    }
}

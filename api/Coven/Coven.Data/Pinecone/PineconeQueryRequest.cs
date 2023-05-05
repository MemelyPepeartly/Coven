using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coven.Data.Pinecone
{
    public class PineconeQueryRequest
    {
        /// <summary>
        /// The filter to apply. You can use vector metadata to limit your search. See https://www.pinecone.io/docs/metadata-filtering/
        /// </summary>
        [JsonProperty("filter")]
        public Filter Filter { get; set; }

        /// <summary>
        /// Indicates whether vector values are included in the response
        /// </summary>
        [JsonProperty("includeValues")]
        public bool IncludeValues { get; set; }
        /// <summary>
        /// Indicates whether metadata is included in the response as well as the ids
        /// </summary>
        [JsonProperty("includeMetadata")]
        public bool IncludeMetadata { get; set; }

        /// <summary>
        /// The query vector. This should be the same length as the dimension of the index being queried. Each query() request can contain only one of the parameters id or vector
        /// </summary>
        [JsonProperty("vector")]
        public List<float> Vector { get; set; }

        /// <summary>
        /// Vector sparse data. Represented as a list of indices and a list of corresponded values, which must be the same length
        /// </summary>
        [JsonProperty("sparseVector")]
        public SparseVector SparseVector { get; set; }

        /// <summary>
        /// The namespace to query
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        /// <summary>
        /// The number of results to return for each query.
        /// </summary>
        [JsonProperty("topK")]
        public int TopK { get; set; }

        /// <summary>
        /// The unique ID of the vector to be used as a query vector. Each query() request can contain only one of the parameters queries, vector, or id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
    }
    public class Filter
    {
        [JsonProperty("genre")]
        public Dictionary<string, List<string>> Genre { get; set; }

        [JsonProperty("year")]
        public Dictionary<string, int> Year { get; set; }
    }

    public class SparseVector
    {
        [JsonProperty("indices")]
        public List<int> Indices { get; set; }

        [JsonProperty("values")]
        public List<float> Values { get; set; }
    }
}

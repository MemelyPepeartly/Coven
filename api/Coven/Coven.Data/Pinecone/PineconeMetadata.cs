using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coven.Data.Pinecone
{
    public abstract class PineconeMetadata { }

    public class DefaultMetadata : PineconeMetadata 
    {
        [JsonProperty("summary")]
        public string Summary { get; set; }
    }

    public class ArticleMetadata : PineconeMetadata
    {
        [JsonProperty("worldId")]
        public string worldId { get; set; }
        [JsonProperty("articleId")]
        public string articleId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("articleType")]
        public string ArticleType { get; set; }
    }
}

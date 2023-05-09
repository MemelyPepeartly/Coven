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
        /// <summary>
        /// Id of the worldanvil world associated with the pinecone entry.
        /// </summary>
        [JsonProperty("worldId")]
        public string WorldId { get; set; }
        /// <summary>
        /// Id of the worldanvil article associated with the pinecone entry.
        /// </summary>
        [JsonProperty("articleId")]
        public string ArticleId { get; set; }
        /// <summary>
        /// String of characters that created the vector associated with the pinecone entry.
        /// </summary>
        [JsonProperty("characterString")]
        public string CharacterString { get; set; }
    }
}

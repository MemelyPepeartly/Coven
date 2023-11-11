using System.Collections.Generic;
using System.Text.Json.Serialization;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;

namespace Coven.Api.Services.Schema
{
    public class SearchModel
    {
        [JsonPropertyName("@search.score")]
        public double searchScore { get; set; }

        [SimpleField(IsKey = true, IsFilterable = true, IsSortable = false)]
        public string worldContentId { get; set; }

        [SearchableField(IsFilterable = true, IsSortable = true)]
        public string worldId { get; set; }

        [SearchableField(IsFilterable = true, IsSortable = true)]
        public string articleId { get; set; }

        [SearchableField(IsFilterable = true, IsSortable = true)]
        public string worldAnvilArticleType { get; set; }

        [SearchableField(IsFilterable = true, IsSortable = true)]
        public string author { get; set; }

        [SearchableField(IsFilterable = false)]
        public string content { get; set; }

        [SearchableField(IsFilterable = true, IsSortable = true)]
        public string articleTitle { get; set; }

        [SearchableField(IsFilterable = true, IsFacetable = true, AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
        public IList<string> people { get; set; }

        [SearchableField(IsFilterable = true, IsFacetable = true, AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
        public IList<string> organizations { get; set; }

        [SearchableField(IsFilterable = true, IsFacetable = true, AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
        public IList<string> locations { get; set; }

        [SearchableField(IsFilterable = true, IsFacetable = true, AnalyzerName = LexicalAnalyzerName.Values.StandardLucene)]
        public IList<string> keyPhrases { get; set; }
    }
}

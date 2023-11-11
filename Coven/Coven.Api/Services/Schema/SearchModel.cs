using Azure.Search.Documents.Indexes.Models;
using Azure.Search.Documents.Indexes;

namespace Coven.Api.Services.Schema
{
    public class SearchModel
    {
        [SimpleField(IsKey = true, IsFilterable = true)]
        public string worldContentId { get; set; }

        [SearchableField(IsFilterable = true)]
        public string worldId { get; set; }

        [SearchableField(IsFilterable = true)]
        public string articleId { get; set; }

        [SearchableField(AnalyzerName = LexicalAnalyzerName.Values.EnLucene)]
        public string content { get; set; }

        [SearchableField(IsFilterable = true, IsFacetable = true)]
        public IList<string> people { get; set; }

        [SearchableField(IsFilterable = true, IsFacetable = true)]
        public IList<string> organizations { get; set; }

        [SearchableField(IsFilterable = true, IsFacetable = true)]
        public IList<string> locations { get; set; }

        [SearchableField(IsFilterable = true, IsFacetable = true)]
        public IList<string> keyPhrases { get; set; }
    }

}

using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Models;

namespace Coven.Api.Services
{
    public interface ISearchService
    {
    }
    public class SearchService : ISearchService
    {
        private readonly SearchClient _searchClient;
        private readonly SearchIndexClient _indexClient;
        private readonly string _indexName;

        public SearchService(IConfiguration configuration)
        {
            string searchServiceName = configuration["SearchServiceName"];
            string adminApiKey = configuration["SearchServiceAdminApiKey"];
            _indexName = configuration["SearchServiceIndexName"];

            string endpoint = $"https://{searchServiceName}.search.windows.net";
            _searchClient = new SearchClient(new Uri(endpoint), _indexName, new AzureKeyCredential(adminApiKey));
            _indexClient = new SearchIndexClient(new Uri(endpoint), new AzureKeyCredential(adminApiKey));
        }

    }
}

using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Models;
using Coven.Api.Services.Schema;
using System.Text.Json.Nodes;

namespace Coven.Api.Services
{
    public interface ISearchService
    {
        Task<SearchResults<SearchModel>> SearchAsync(string searchText, SearchOptions options = null);
    }
    public class SearchService : ISearchService
    {
        private readonly SearchClient _searchClient;
        private readonly string _indexName;

        public SearchService(IConfiguration configuration)
        {
            string searchServiceName = configuration["SearchServiceName"];
            string adminApiKey = configuration["SearchServiceAdminApiKey"];
            _indexName = configuration["SearchServiceIndexName"];

            Uri endpoint = new Uri($"https://{searchServiceName}.search.windows.net/");
            AzureKeyCredential credential = new AzureKeyCredential(adminApiKey);
            _searchClient = new SearchClient(endpoint, _indexName, credential);
        }

        public async Task<SearchResults<SearchModel>> SearchAsync(string searchText, SearchOptions options = null)
        {
            options ??= new SearchOptions();
            // Set default options here if needed

            try
            {

                return await _searchClient.SearchAsync<SearchModel>(searchText, options);
            }
            catch (RequestFailedException e)
            {
                // Handle or log the exception as appropriate
                throw;
            }
        }

    }
}

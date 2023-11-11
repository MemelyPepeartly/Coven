using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Coven.Api.Services;
using Coven.Api.Services.Schema;
using Coven.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

namespace Coven.Api.Controllers
{
    public class SearchController : Controller
    {
        private readonly IWorldAnvilService _worldAnvilService;
        private readonly IRepository _repository;
        private readonly ISearchService _searchService;

        public SearchController(IWorldAnvilService worldAnvilService, IRepository repository, ISearchService searchService)
        {
            _worldAnvilService = worldAnvilService;
            _repository = repository;
            _searchService = searchService;
        }

        [HttpPost("Search")]
        public async Task<ActionResult> Search([FromBody]string query)
        {

            var searchResults = await _searchService.SearchAsync(query, new SearchOptions
            {
                QueryType = SearchQueryType.Semantic, // Enable semantic search
                SemanticSearch = new SemanticSearchOptions()
                {
                    SemanticConfigurationName = "worldanvil-semantic-search"
                },
                Size = 10,
                IncludeTotalCount = true,
                Select =
                    {
                        "worldContentId",
                        "content",
                        "articleId",
                        "worldId",
                        "people",
                        "organizations",
                        "locations",
                        "keyphrases",
                        "articleTitle",
                        "worldAnvilArticleType",
                        "author"
                    }
            });

            // Initialize a list to hold the results
            List<SearchModel> results = new List<SearchModel>();

            // Iterate over the search results
            await foreach (SearchResult<SearchModel> result in searchResults.GetResultsAsync())
            {
                // Access the document
                SearchModel document = result.Document;

                // Add the document to the results list
                results.Add(document);
            }



            return Ok(results);
        }
    }
}

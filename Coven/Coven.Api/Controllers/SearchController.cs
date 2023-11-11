using Coven.Api.Services;
using Coven.Data.Repository;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(await _searchService.SearchAsync(query));
        }
    }
}

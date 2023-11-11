using Coven.Api.Services;
using Coven.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Coven.Api.Controllers
{
    public class SearchController : Controller
    {
        private readonly IWorldAnvilService _worldAnvilService;
        private readonly IRepository _repository;

        public SearchController(IWorldAnvilService worldAnvilService, IRepository repository)
        {
            _worldAnvilService = worldAnvilService;
            _repository = repository;
        }
    }
}

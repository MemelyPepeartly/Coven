using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Coven.Api.Services;
using Coven.Logic.DTO.WorldAnvil;

namespace Tardigrade.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorldController : ControllerBase
    {
        private readonly IWorldAnvilService WorldAnvilService;

        public WorldController(IWorldAnvilService _worldAnvilService)
        {
            WorldAnvilService = _worldAnvilService;
        }

        [HttpGet("GetWorldInfo")]
        public async Task<ActionResult> GetWorldInfo() 
        {
            return Ok(await WorldAnvilService.GetWorlds());
        }

        [HttpGet("GetAnvilUser")]
        public async Task<ActionResult> GetAnvilUser()
        {
            return Ok(await WorldAnvilService.GetUser());
        }

        [HttpGet("GetWorldArticleSummary")]
        public async Task<ActionResult> GetWorldArticleSummary(Guid worldId)
        {
            WorldArticlesSummary result = await WorldAnvilService.GetArticles(worldId);
            result.articles = result.articles.OrderBy(a => a.title).ToList();
            return Ok(result);
        }

        [HttpGet("GetWorldArticles/{articleId}")]
        public async Task<ActionResult> GetWorldArticle(Guid articleId)
        {
            return Ok(await WorldAnvilService.GetArticle(articleId));
        }
    }
}

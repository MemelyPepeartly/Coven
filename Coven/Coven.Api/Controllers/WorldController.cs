using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Coven.Api.Services;
using Coven.Logic.DTO.WorldAnvil;
using Coven.Data.Repository;

namespace Tardigrade.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorldController : ControllerBase
    {
        private readonly IWorldAnvilService WorldAnvilService;
        private readonly IRepository Repository;

        public WorldController(IWorldAnvilService _worldAnvilService, IRepository _repository)
        {
            WorldAnvilService = _worldAnvilService;
            Repository = _repository;
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

        [HttpGet("GetWorldArticles/{articleId}")]
        public async Task<ActionResult> GetWorldArticle(Guid articleId)
        {
            return Ok(await WorldAnvilService.GetArticle(articleId));
        }

        /// <summary>
        /// Syncs the worldanvil world list for a coven user and adds it to the user's world database list
        /// </summary>
        /// <returns></returns>
        [HttpPost("SyncWorlds")]
        public async Task<ActionResult> SyncWorlds(Guid userId)
        {
            WorldSegmentSummary worlds = await WorldAnvilService.GetWorlds();

            // Adds all the worlds to the database
            var success = await Repository.CreateWorlds(userId, worlds.worlds);

            if(success)
            {
                return Ok(await Repository.GetWorlds(userId));
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{worldId}/GetWorldArticleSummary")]
        public async Task<ActionResult> GetWorldArticleSummary(Guid worldId)
        {
            WorldArticlesSummary result = await WorldAnvilService.GetWorldArticlesSummary(worldId);
            result.articles = result.articles.OrderBy(a => a.title).ToList();
            return Ok(result);
        }

        [HttpGet("{worldId}/GetWorldArticleMetas")]
        public async Task<ActionResult> GetWorldArticleMetas(Guid worldId)
        {
            return Ok(await WorldAnvilService.GetArticleMetas(worldId));
        }

        [HttpGet("{worldId}/GetPineconeMetadata")]
        public async Task<ActionResult> GetPineconeMetadata(Guid worldId)
        {
            return Ok(await Repository.GetWorldPineconeMetadatum(worldId));
        }
    }
}

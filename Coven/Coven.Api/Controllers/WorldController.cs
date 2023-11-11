using Coven.Api.Services;
using Coven.Data.Repository;
using Coven.Data.Repository.Models;
using Coven.Logic.Base_Types;
using Coven.Logic.DTO.WorldAnvil;
using Coven.Logic.Meta_Objects;
using Microsoft.AspNetCore.Mvc;

namespace Tardigrade.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorldController : ControllerBase
    {
        private readonly IWorldAnvilService _worldAnvilService;
        private readonly IRepository _repository;

        public WorldController(IWorldAnvilService worldAnvilService, IRepository repository)
        {
            _worldAnvilService = worldAnvilService;
            _repository = repository;
        }

        [HttpGet("GetWorldInfo")]
        public async Task<ActionResult> GetWorldInfo() 
        {
            return Ok(await _worldAnvilService.GetWorlds());
        }

        [HttpGet("GetAnvilUser")]
        public async Task<ActionResult> GetAnvilUser()
        {
            return Ok(await _worldAnvilService.GetUser());
        }

        [HttpGet("GetWorldArticle/{articleId}")]
        public async Task<ActionResult> GetWorldArticle(Guid articleId)
        {
            return Ok(await _worldAnvilService.GetArticle(articleId));
        }

        [HttpGet("GetWorldArticleContent/{articleId}")]
        public async Task<ActionResult<string>> GetWorldArticleContent(Guid articleId)
        {
            var article = await _worldAnvilService.GetArticle(articleId);
            return Ok(article.content);
        }


        /// <summary>
        /// Syncs the worldanvil world list for a coven user and adds it to the user's world database list
        /// </summary>
        /// <returns></returns>
        [HttpPost("SyncWorlds")]
        public async Task<ActionResult> SyncWorlds(Guid userId)
        {
            WorldSegmentSummary worlds = await _worldAnvilService.GetWorlds();

            // Adds all the worlds to the database
            var success = await _repository.CreateWorlds(userId, worlds.worlds);

            if(success)
            {
                return Ok(await _repository.GetWorlds(userId));
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{worldId}/SyncWorldContentToDatabase")]
        public async Task<ActionResult> SyncWorldContentToDatabase(Guid worldId)
        {
            List<ArticleMeta> metas = await _worldAnvilService.GetArticleMetas(worldId);

            var articleList = new List<IndexTableModel>();

            foreach(ArticleMeta meta in metas)
            {
                Article article = await _worldAnvilService.GetArticle(meta.id);
                articleList.Add(new IndexTableModel()
                {
                    worldId = worldId,
                    articleId = article.id,
                    articleTitle = article.title,
                    worldAnvilArticleType = ArticleParser.GetArticleTypeFromUrl(article.url),
                    author = metas.First(m => m.id == article.id).author.username,
                    content = ArticleParser.RemoveBBCode(article.content)
                });
            }

            return Ok(await _repository.CreateWorldContentEntries(articleList));
        }

        [HttpGet("{worldId}/GetWorldArticleSummary")]
        public async Task<ActionResult> GetWorldArticleSummary(Guid worldId)
        {
            WorldArticlesSummary result = await _worldAnvilService.GetWorldArticlesSummary(worldId);
            result.articles = result.articles.OrderBy(a => a.title).ToList();
            return Ok(result);
        }

        [HttpGet("{worldId}/GetWorldArticleMetas")]
        public async Task<ActionResult> GetWorldArticleMetas(Guid worldId)
        {
            return Ok((await _worldAnvilService.GetArticleMetas(worldId)).Select(a => new
            {
                id = a.id,
                title = a.title,
                url = a.url,
                author = a.author.username
            }));
        }
    }
}

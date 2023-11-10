using Coven.Data.Meta_Objects;
using Coven.Logic.Base_Types;
using Coven.Logic.DTO.WorldAnvil;
using Coven.Logic.Meta_Objects;

namespace Coven.Api.Services
{
    public interface IWorldAnvilService
    {
        Task<Author> GetUser();
        Task<WorldSegmentSummary> GetWorlds();
        Task<WorldSegment> GetWorld(Guid worldId);
        Task<WorldArticlesSummary> GetWorldArticlesSummary(Guid worldId);
        Task<Article> GetArticle(Guid articleId);
        Task<List<ArticleMeta>> GetArticleMetas(Guid worldId);
    }
}

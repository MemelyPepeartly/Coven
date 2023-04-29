using Coven.Logic.Base_Types;
using Coven.Logic.DTO.WorldAnvil;

namespace Coven.Api.Services
{
    public interface IWorldAnvilService
    {
        Task<Author> GetUser();
        Task<WorldsSummary> GetWorlds();
        Task<WorldArticlesSummary> GetArticles(Guid worldId);
        Task<Article> GetArticle(Guid articleId);
    }
}

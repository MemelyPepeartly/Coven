using Coven.Logic.Base_Types;
using Coven.Logic.DTO.WorldAnvil;

namespace Coven.Api.Services
{
    public interface IWorldAnvilService
    {
        Task<Author> GetUser();
        Task<WorldAnvilUserWorlds> GetWorlds();
        Task<WorldAnvilUserArticles> GetArticles(Guid worldId);
        Task<Article> GetArticle(Guid articleId);
    }
}

using Coven.Logic.DTO.WorldAnvil;
using Coven.Logic.Request_Models.Get;

namespace Coven.Api.Services
{
    public interface IWorldAnvilService
    {
        Task<WorldAnvilUser> GetUser();
        Task<WorldAnvilWorld> GetWorlds();
        Task<ArticleDTO> GetArticles(Guid worldId);
    }
}

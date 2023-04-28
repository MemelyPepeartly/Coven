using Coven.Logic.DTO.WorldAnvil;

namespace Coven.Api.Services
{
    public interface IWorldAnvilService
    {
        Task<WorldAnvilUser> GetUser();
        Task<WorldAnvilUserWorlds> GetWorlds();
        Task<WorldAnvilUserArticles> GetArticles(Guid worldId);
    }
}

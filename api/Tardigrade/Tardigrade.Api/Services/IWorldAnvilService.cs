using Tardigrade.Logic.DTO.WorldAnvil;

namespace Tardigrade.Api.Services
{
    public interface IWorldAnvilService
    {
        Task<object> GetWorld(Guid worldId);
        Task<ArticleDTO> GetArticles(Guid worldId);
    }
}

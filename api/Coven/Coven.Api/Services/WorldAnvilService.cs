using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net.Http.Headers;
using System.Text;
using Tardigrade.Logic.DTO.WorldAnvil;

namespace Tardigrade.Api.Services
{
    public class WorldAnvilService : IWorldAnvilService
    {
        private HttpClient WorldAnvilClient;
        private readonly IConfiguration _config;

        public WorldAnvilService(IConfiguration config) 
        { 
            _config = config;

            WorldAnvilClient = new HttpClient();
            WorldAnvilClient.BaseAddress = new Uri("https://www.worldanvil.com/api/aragorn/");
            WorldAnvilClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            WorldAnvilClient.DefaultRequestHeaders.Add("x-auth-token", _config["WorldAnvilToken"]);
        }
        public async Task<ArticleDTO> GetArticles(Guid worldId)
        {
            throw new NotImplementedException();
        }

        public async Task<object> GetWorld(Guid worldId)
        {
            var data = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new { worldId = worldId })));
            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return await WorldAnvilClient.GetAsync($"world/dbd8562f-06be-4063-9f6d-e90bf89c761e");
        }
    }
}

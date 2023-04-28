using Azure.Core;
using Coven.Logic.DTO.WorldAnvil;
using Coven.Logic.Request_Models.Get;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;

namespace Coven.Api.Services
{
    public class WorldAnvilService : IWorldAnvilService
    {
        private readonly IConfiguration _config;
        private HttpClient client;
        public WorldAnvilService(IConfiguration config) 
        { 
            _config = config;
            client = new HttpClient();

            client.BaseAddress = new Uri("https://www.worldanvil.com/api/aragorn/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("x-auth-token", _config["WorldAnvilToken"]);
            client.DefaultRequestHeaders.Add("x-application-key", _config["WorldAnvilAppKey"]);
        }
        public async Task<ArticleDTO> GetArticles(Guid worldId)
        {
            throw new NotImplementedException();
        }

        public async Task<WorldAnvilWorld> GetWorlds()
        {
            var worlds = new WorldAnvilWorld();
            HttpResponseMessage response = await client.GetAsync($"user/{(await GetUser()).id}/worlds");
            if (response.IsSuccessStatusCode)
            {
                worlds = await response.Content.ReadAsAsync<WorldAnvilWorld>();
            }
            return worlds;
        }

        public async Task<WorldAnvilUser> GetUser()
        {
            WorldAnvilUser user = new WorldAnvilUser();
            HttpResponseMessage response = await client.GetAsync("user");
            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<WorldAnvilUser>();
            }
            else
            {
                throw new Exception("oop");
            }
            return user;
        }
    }
}

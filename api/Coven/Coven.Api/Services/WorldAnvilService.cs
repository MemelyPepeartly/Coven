using Azure.Core;
using Coven.Logic.DTO.WorldAnvil;
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
        public async Task<WorldAnvilUserArticles> GetArticles(Guid worldId)
        {
            WorldAnvilUserArticles articles = new WorldAnvilUserArticles();
            HttpResponseMessage response = await client.GetAsync($"world/{worldId}/articles");
            if (response.IsSuccessStatusCode)
            {
                articles = await response.Content.ReadAsAsync<WorldAnvilUserArticles>();
            }
            return articles;
        }

        public async Task<WorldAnvilUserWorlds> GetWorlds()
        {
            WorldAnvilUserWorlds worlds = new WorldAnvilUserWorlds();
            HttpResponseMessage response = await client.GetAsync($"user/{(await GetUser()).id}/worlds");
            if (response.IsSuccessStatusCode)
            {
                worlds = await response.Content.ReadAsAsync<WorldAnvilUserWorlds>();
            }
            return worlds;
        }

        public async Task<Author> GetUser()
        {
            Author user = new Author();
            HttpResponseMessage response = await client.GetAsync("user");
            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<Author>();
            }
            else
            {
                throw new Exception("oop");
            }
            return user;
        }
    }
}

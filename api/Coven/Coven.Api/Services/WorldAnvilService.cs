using Azure.Core;
using Coven.Logic.Base_Types;
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
        public async Task<WorldArticlesSummary> GetWorldArticlesSummary(Guid worldId)
        {
            WorldArticlesSummary articles = new WorldArticlesSummary();
            HttpResponseMessage response = await client.GetAsync($"world/{worldId}/articles");
            if (response.IsSuccessStatusCode)
            {
                articles = await response.Content.ReadAsAsync<WorldArticlesSummary>();
            }
            return articles;
        }
        public async Task<Article> GetArticle(Guid articleId)
        {
            Article article = new Article();
            HttpResponseMessage response = await client.GetAsync($"article/{articleId}");
            if (response.IsSuccessStatusCode)
            {
                article = await response.Content.ReadAsAsync<Article>();
            }
            return article;
        }

        public async Task<WorldsSummary> GetWorlds()
        {
            WorldsSummary worlds = new WorldsSummary();
            HttpResponseMessage response = await client.GetAsync($"user/{(await GetUser()).id}/worlds");
            if (response.IsSuccessStatusCode)
            {
                worlds = await response.Content.ReadAsAsync<WorldsSummary>();
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
            return user;
        }

        public async Task<object> GetTest(Guid articleId)
        {
            string article = "";
            HttpResponseMessage response = await client.GetAsync($"article/{articleId}");
            if (response.IsSuccessStatusCode)
            {
                article = await response.Content.ReadAsStringAsync();
            }
            return article;
        }
    }
}

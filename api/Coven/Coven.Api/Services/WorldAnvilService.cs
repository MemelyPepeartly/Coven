using Azure.Core;
using Coven.Logic.Base_Types;
using Coven.Logic.DTO.WorldAnvil;
using Coven.Logic.Meta_Objects;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerUI;
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

        public async Task<WorldSegmentSummary> GetWorlds()
        {
            WorldSegmentSummary worlds = new WorldSegmentSummary();
            HttpResponseMessage response = await client.GetAsync($"user/{(await GetUser()).id}/worlds");
            if (response.IsSuccessStatusCode)
            {
                worlds = await response.Content.ReadAsAsync<WorldSegmentSummary>();
            }
            return worlds;
        }
        public async Task<WorldSegment> GetWorld(Guid worldId)
        {
            WorldSegmentSummary worlds = new WorldSegmentSummary();
            HttpResponseMessage response = await client.GetAsync($"user/{(await GetUser()).id}/worlds");
            if (response.IsSuccessStatusCode)
            {
                worlds = await response.Content.ReadAsAsync<WorldSegmentSummary>();
            }
            return worlds.worlds.First(w => w.id == worldId);
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

        public async Task<List<ArticleMeta>> GetArticleMetas(Guid worldId)
        {
            WorldArticlesSummary summary = new WorldArticlesSummary();
            List<ArticleMeta> articles = new List<ArticleMeta>();

            bool moreExists = true;
            int page = 1;
            while(moreExists)
            {
                HttpResponseMessage response = await client.GetAsync($"world/{worldId}/articles?offset={page * 50}");
                if (response.IsSuccessStatusCode)
                {
                    summary = await response.Content.ReadAsAsync<WorldArticlesSummary>();
                }

                articles = articles
                    .Concat(summary.articles)
                    .ToList();
                page++;

                // If the last call had less than 50, we've probably reached the end, so break
                if (summary.articles.Count < 50)
                {
                    moreExists = false;
                }

                // Minor bit of content checking in case something goes wrong. 30 requests should be more than enough
                if(page > 30)
                {
                    break;
                }
            }

            return articles
                .OrderBy(a => a.title)
                .ToList();
        }
    }
}

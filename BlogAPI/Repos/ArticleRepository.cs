using System.Text;
using Alachisoft.NCache.Licensing.DOM;
using BlogAPI.Context;
using BlogAPI.DTOs;
using BlogAPI.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace BlogAPI.Repos;

public class ArticleRepository : IArticleRepository
{
    private readonly IDistributedCache _cache;

    public ArticleRepository(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<IEnumerable<ArticleModel>> GetAll()
    {
        var response = new List<ArticleModel>();
        
        byte[] articlesInRedis = await _cache.GetAsync("Article");
        if ((articlesInRedis?.Count() ?? 0) > 0) 
        {
            var articleString = Encoding.UTF8.GetString(articlesInRedis);
            var allArticles = JsonConvert.DeserializeObject<List<Article>>(articleString);
				
            foreach (var article in allArticles)
            {
                var tempresp = new ArticleModel()
                {
                    Id = article.Id,
                    Title = article.Title,
                    Body = article.Body,
                    PublishedDate = article.Published
                };
                response.Add(tempresp);
            }
        } 
        
        return new List<ArticleModel>();
    }
}
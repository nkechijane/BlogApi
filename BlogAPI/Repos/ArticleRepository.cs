using System.Text;
using BlogAPI.DTOs;
using BlogAPI.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace BlogAPI.Repos;

public class ArticleRepository : IArticleRepository
{
    private readonly IDistributedCache _cache;
    private readonly BlogConfig _blogConfig;

    public ArticleRepository(IDistributedCache cache, BlogConfig blogConfig)
    {
        _cache = cache;
        _blogConfig = blogConfig;
    }

    public async Task<List<ArticleModel>> GetAll()
    {
        var response = new List<ArticleModel>();
        var allArticles = await FilterByKey("*",0,true);
        foreach (var key in allArticles )
        {
            var author = await Get(new Guid(key));
            response.Add(author);
        }
        return response;
    }

    public async Task<string> Add(SaveArticleModel newArticle)
    {
        var article = new Article()
        {
            Id = Guid.NewGuid(),
            Title = newArticle.Title,
            Body = newArticle.Body,
            Published = DateTime.Now
        };
        await Save(article);

        return ("Article Added Successfully");
    }

    public async Task<ArticleModel> Get(Guid id)
    {
        var article = await GetArticleById(id.ToString());
        if (!string.IsNullOrEmpty(article.Body))
        {
            var response = new ArticleModel()
            {
                Id = article.Id,
                Title = article.Title,
                Body = article.Body,
                PublishedDate = DateTime.Now
            };
            return response;
        }
        return new ArticleModel();
    }

    public async Task<bool> Update(ArticleModel newArticle)
    {
        var articleToUpdate = GetArticleById(newArticle.Id.ToString()).Result;
        if (string.IsNullOrEmpty(articleToUpdate.Body))
            return false;

        articleToUpdate.LastEdited = DateTime.Now;
        articleToUpdate.Title = newArticle.Title;
        articleToUpdate.Body = newArticle.Body;
        
        _cache.Remove(newArticle.Id.ToString());
        await Save(articleToUpdate);
        return true;
    }
    
    private async Task<List<string>> FilterByKey(string keyStr, int dbIndex = 0, bool trimRedisInstanceName = true)
    {
        var conn = await ConnectionMultiplexer.ConnectAsync(_blogConfig.REDIS_CACHE_CONN_STRING);

        var db = conn.GetDatabase(dbIndex);
        var listResult = new List<string>();

        foreach (var endPoint in conn.GetEndPoints())
        {
            var server = conn.GetServer(endPoint);
            var allkeys = server.Keys(dbIndex, keyStr);
            foreach (var key in allkeys)
            {
                if (trimRedisInstanceName)
                {
                    listResult.Add(key.ToString().Replace("master", ""));
                }
                else
                {
                    listResult.Add(key);
                }
                //var val = db.StringGet(key);
                Console.WriteLine($"key: {key}, value:");
            }
        }
        return listResult;
    }

    private async Task<Article> GetArticleById(string id)
    {
        byte[] articlesInRedis = await _cache.GetAsync(id);
        if ((articlesInRedis?.Count() ?? 0) > 0)
        {
            var articleString = Encoding.UTF8.GetString(articlesInRedis);
            var allArticles = JsonConvert.DeserializeObject<Article>(articleString);

            return allArticles;
        }

        return new Article();
    }

    private async Task Save(Article newArticle)
    {
        var serializedArticle = JsonConvert.SerializeObject(newArticle);

        byte[] articleToCache = Encoding.UTF8.GetBytes(serializedArticle);
        await _cache.SetAsync(newArticle.Id.ToString(), articleToCache);
    }
}
using System.Text;
using BlogAPI.DTOs;
using BlogAPI.Models;
using Microsoft.Extensions.Caching.Distributed;
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
            var articleString = Encoding.UTF8.GetString(
                articlesInRedis);
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

        return article.Id.ToString();
    }

    public async Task<ArticleModel> Get(Guid id)
    {
        var article = GetArticleById(id.ToString()).Result;
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
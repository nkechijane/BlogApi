using BlogAPI.Context;
using BlogAPI.DTOs;
using BlogAPI.Models;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace BlogAPI.Repos;

public class ArticleRepository : IArticleRepository
{
    private readonly IMemoryCache _memoryCache;

    public ArticleRepository(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public IEnumerable<ArticleModel> GetAll()
    {
        var response = new List<ArticleModel>();
        var allArticles = new List<Article>();
        foreach (var key in _memoryCache.GetKeys())
        {
            var tempArticle = JsonConvert.DeserializeObject<Article>(_memoryCache.Get<string>(key));
            allArticles.Add(tempArticle);
        }

        Task.WaitAll();

        if (!allArticles.Any())
            return new List<ArticleModel>();
        
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

        return response;
    }

    public bool Add(SaveArticleModel newArticle)
    {
        var article = new Article()
        {
            Id = Guid.NewGuid(),
            Title = newArticle.Title,
            Body = newArticle.Body,
            Published = DateTime.Now
        };
        var tempArticle = JsonConvert.SerializeObject(article);
        
        _memoryCache.Set(article.Id, tempArticle);
        
        return true;
    }

    public ArticleModel Get(Guid id)
    {
       var tempArticle = _memoryCache.Get<string>(id);

       if (tempArticle == null)
           return new ArticleModel();

       var article = JsonConvert.DeserializeObject<Article>(tempArticle);
       
       var response = new ArticleModel()
       {
           Id = article.Id,
           Title = article.Title,
           Body = article.Body,
           PublishedDate = DateTime.Now
       };
       return response;
    }
    
    public bool Update(ArticleModel newArticle)
    {
        //var dbresult = _dbContext.Articles.FirstOrDefault(a => a.Id == newArticle.Id);
        var tempArticle = _memoryCache.Get<string>(newArticle.Id);
        if (tempArticle == null)
            return false;

        var dbresult = JsonConvert.DeserializeObject<Article>(tempArticle);

        dbresult.LastEdited = DateTime.Now;
        dbresult.Title = newArticle.Title;
        dbresult.Body = newArticle.Body;
        
        _memoryCache.Remove(newArticle.Id);
        _memoryCache.Set(newArticle.Id, dbresult);
        //_dbContext.Update(dbresult);
        //return _dbContext.SaveChanges() > 0;
        return true;
    }
}
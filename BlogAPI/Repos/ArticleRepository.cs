using BlogAPI.Context;
using BlogAPI.DTOs;
using BlogAPI.Models;

namespace BlogAPI.Repos;

public class ArticleRepository : IArticleRepository
{
    private readonly AppDbContext _dbContext;

    public ArticleRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<ArticleModel> GetAll()
    {
        var response = new List<ArticleModel>();

        var allArticles = _dbContext.Articles;
        
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
        _dbContext.Articles.Add(article);
        _dbContext.SaveChanges();
        return true;
    }

    public ArticleModel Get(Guid id)
    {
       var article = _dbContext.Articles.FirstOrDefault(a => a.Id.CompareTo(id) == 0);

       if (article == null)
           return new ArticleModel();
       
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
        var dbresult = _dbContext.Articles.FirstOrDefault(a => a.Id == newArticle.Id);
            
        if (dbresult == null)
            return false;

        dbresult.LastEdited = DateTime.Now;
        dbresult.Title = newArticle.Title;
        dbresult.Body = newArticle.Body;
        _dbContext.Update(dbresult);
        return _dbContext.SaveChanges() > 0;
    }
}
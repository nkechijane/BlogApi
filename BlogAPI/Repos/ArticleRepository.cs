using BlogAPI.DTOs;
using BlogAPI.Models;

namespace BlogAPI.Repos;

public class ArticleRepository : IArticleRepository
{
    public List<Article> _allArticles;

    public ArticleRepository()
    {
        _allArticles ??= new List<Article>()
        {
            new()
            {
                Id = Guid.NewGuid(), Title = "The GirlChild", Body = "Train a child, train a nation",
                Published = DateTime.Now
            },

            new()
            {
                Id = Guid.NewGuid(), Title = "Dark", Body = "Dark nation", Published = DateTime.Now
            }
        };
    }

    public IEnumerable<ArticleModel> GetAll()
    {
        var response = new List<ArticleModel>();
        foreach (var article in _allArticles)
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
        _allArticles.Add(article);
        return true;
    }

    public ArticleModel Get(Guid id)
    {
       var article = _allArticles.FirstOrDefault(a => a.Id.CompareTo(id) == 0);

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
    
    public ArticleModel Update(ArticleModel newArticle)
    {
        foreach (var article in _allArticles.Where(a => a.Id.CompareTo(newArticle.Id) == 0))
        {
            article.LastEdited = DateTime.Now;
            article.Title = newArticle.Title;
            article.Body = newArticle.Body;
            article.Published = newArticle.PublishedDate;
        }
        return newArticle;
    }
}
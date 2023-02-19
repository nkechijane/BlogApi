using BlogAPI.Models;

namespace BlogAPI.Repos;

public class ArticleRepository : IArticleRepository
{
    private List<Article> _allArticles;

    public ArticleRepository()
    {
        _allArticles = new List<Article>()
        {
            new()
            {
                Id = 1, Title = "The GirlChild", Body = "Train a child, train a nation", Published = DateTime.Now
            },
            
            new()
            {
                Id = 1, Title = "Dark", Body = "Dark nation", Published = DateTime.Now
            }
        };
    }
    public IEnumerable<Article> GetAll()
    {
        return _allArticles;
    }

    public Article Add(Article article)
    {
        article.Published = DateTime.Now;
        _allArticles.Add(article);
        return article;
    }

    public Article Get(int id)
    {
       var article = _allArticles.FirstOrDefault(a => a.Id == id);
       return article;
    }
    
    public Article Update(Article newArticle)
    {
        foreach (var article in _allArticles.Where(a => a.Id == newArticle.Id))
        {
            article.LastEdited = DateTime.Now;
            article.Title = newArticle.Title;
            article.Body = newArticle.Body;
            article.Published = newArticle.Published;
        }
        return newArticle;
    }
}
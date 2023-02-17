using BlogAPI.Models;

namespace BlogAPI.Repos;

public class ArticleRepository : IArticleRepository
{
    private List<Article> _allArticles;

    public ArticleRepository()
    {
        _allArticles = new List<Article>()
        {
            new Article
            {
                Id = 1, Title = "The GirlChild", Body = "Train a child, train a nation", Published = DateTime.Now
            },
            
            new Article
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
        _allArticles.Add(article);
        return article;
    }
}
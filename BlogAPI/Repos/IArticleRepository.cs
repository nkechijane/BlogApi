using BlogAPI.Models;

namespace BlogAPI.Repos;

public interface IArticleRepository
{
    IEnumerable<Article> GetAll();
    Article Add(Article article);
}
using BlogAPI.DTOs;
using BlogAPI.Models;

namespace BlogAPI.Repos;

public interface IArticleRepository
{
    IEnumerable<ArticleModel> GetAll();
    bool Add(SaveArticleModel article);
    ArticleModel Get(Guid id);
    bool Update(ArticleModel payload);
}
using BlogAPI.DTOs;
using BlogAPI.Models;

namespace BlogAPI.Repos;

public interface IArticleRepository
{
    IEnumerable<ArticleModel> GetAll();
    bool Add(SaveArticleModel article);
    ArticleModel Get(Guid id);
    ArticleModel Update(ArticleModel payload);
}
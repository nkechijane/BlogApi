using BlogAPI.DTOs;
using BlogAPI.Models;

namespace BlogAPI.Repos;

public interface IArticleRepository
{
    Task<IEnumerable<ArticleModel>> GetAll();
    Task<bool> Add(SaveArticleModel newArticle);
}
using BlogAPI.DTOs;
using BlogAPI.Models;

namespace BlogAPI.Repos;

public interface IArticleRepository
{
    Task<IEnumerable<ArticleModel>> GetAll();
    Task<string> Add(SaveArticleModel newArticle);
    Task<ArticleModel> Get(Guid id);
    Task<bool> Update(ArticleModel articleToUpdate);
}
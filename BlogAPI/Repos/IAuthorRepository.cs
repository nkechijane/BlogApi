using BlogAPI.DTOs;
using BlogAPI.Models;

namespace BlogAPI.Repos;

public interface IAuthorRepository
{
    Task<IEnumerable<AuthorModel>> GetAll();
    Task<string> Add(SaveAuthorModel newArticle);
    Task<AuthorModel> Get(Guid id);
    Task<bool> Update(AuthorModel authorToUpdate);
}
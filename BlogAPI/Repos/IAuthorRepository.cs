using BlogAPI.DTOs;

namespace BlogAPI.Repos;

public interface IAuthorRepository
{
    Task<List<AuthorModel>> GetAll();
    Task<string> Add(SaveAuthorModel newArticle);
    Task<AuthorModel> Get(Guid id);
    Task<bool> Update(AuthorModel authorToUpdate);
}
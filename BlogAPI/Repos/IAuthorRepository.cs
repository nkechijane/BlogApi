using BlogAPI;
using BlogAPI.Models;

namespace DefaultNamespace;

public interface IAuthorRepository
{
    bool Save(SaveAuthorModel author);
    IEnumerable<AuthorModel> GetAll();
    AuthorModel Get(Guid id);
    AuthorModel Update(AuthorModel newAuthor);
}
 
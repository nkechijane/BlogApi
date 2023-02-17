using BlogAPI.Models;

namespace DefaultNamespace;

public interface IAuthorRepository
{
    Author Save(Author author);
    IEnumerable<Author> GetAll();
    Author Get(int id);
    Author Update(Author newAuthor);
}
 
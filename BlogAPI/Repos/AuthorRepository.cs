using BlogAPI.Models;
using DefaultNamespace;

namespace BlogAPI.Repos;

public class AuthorRepository : IAuthorRepository
{
    private List<Author> _allAuthors;

    public AuthorRepository()
    {
        _allAuthors = new List<Author>()
        {
            new() {Id = 1, FirstName = "Tina", MiddleName = "Doe", LastName = "Effiong"},
            new() {Id = 2, FirstName = "Forbes", MiddleName = "Jeff", LastName = "Arthor"},
            new() {Id = 3, FirstName = "Jeff", MiddleName = "J.", LastName = "Bezzos"}
        };
    }

    public Author Save(Author newAuthor)
    {
        var author = new Author()
        {
            Id = newAuthor.Id,
            FirstName = newAuthor.FirstName,
            MiddleName = newAuthor.MiddleName,
            LastName = newAuthor.LastName
        };
        _allAuthors.Add(author);
        return author;
    }

    public IEnumerable<Author> GetAll()
    {
        return _allAuthors;
    }

    public Author Get(int id)
    {
        var author = _allAuthors.FirstOrDefault(a => a.Id == id);
        return author ?? new Author();
    }

    public Author Update(Author newAuthor)
    {
        foreach (var oldAuthor in _allAuthors.Where(a => a.Id == newAuthor.Id))
        {
            oldAuthor.FirstName = newAuthor.FirstName;
            oldAuthor.LastName = newAuthor.LastName;
            oldAuthor.MiddleName = newAuthor.MiddleName;
        }
        return newAuthor;
    }
}
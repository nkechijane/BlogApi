using BlogAPI.DTOs;
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
            new() {Id = Guid.NewGuid(), FirstName = "Tina", MiddleName = "Doe", LastName = "Effiong"},
            new() {Id = Guid.NewGuid(),FirstName = "Forbes", MiddleName = "Jeff", LastName = "Arthor"},
            new() {Id = Guid.NewGuid(),FirstName = "Jeff", MiddleName = "J.", LastName = "Bezzos"}
        };
    }

    public bool Save(SaveAuthorModel newAuthor)
    {
        var author = new Author()
        {
            Id = Guid.NewGuid(),
            FirstName = newAuthor.FirstName,
            MiddleName = newAuthor.MiddleName,
            LastName = newAuthor.LastName
        };
        _allAuthors.Add(author);
        return true;
    }

    public IEnumerable<AuthorModel> GetAll()
    {
        var response = new List<AuthorModel>();
        foreach (var author in _allAuthors)
        {
            var tempresp = new AuthorModel()
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                MiddleName = author.MiddleName
            };
            response.Add(tempresp);
        }

        return response;
    }

    public AuthorModel Get(Guid id)
    {
        var author = _allAuthors.FirstOrDefault(a => a.Id == id);
        var response = new AuthorModel()
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName,
            MiddleName = author.MiddleName
        };
        return response;
    }

    public AuthorModel Update(AuthorModel newAuthor)
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
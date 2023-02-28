using BlogAPI.Context;
using BlogAPI.DTOs;
using BlogAPI.Models;
using DefaultNamespace;

namespace BlogAPI.Repos;

/*public class AuthorRepository : IAuthorRepository
{
    private readonly AppDbContext _dbContext;
    public AuthorRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
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
        _dbContext.Add(author);
        _dbContext.SaveChanges();
        return true;
    }

    public IEnumerable<AuthorModel> GetAll()
    {
        var response = new List<AuthorModel>();
        var allAuthors = _dbContext.Authors;
        
        if (!allAuthors.Any())
            return new List<AuthorModel>();
        
        foreach (var author in _dbContext.Authors)
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
        var author = _dbContext.Authors.FirstOrDefault(a => a.Id.CompareTo(id) == 0);
        if (author == null)
            return new AuthorModel();
        
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
        var oldAuthor = _dbContext.Authors.FirstOrDefault(a => a.Id.CompareTo(newAuthor.Id) == 0);
        if (oldAuthor != null)
        {
            oldAuthor.FirstName = newAuthor.FirstName;
            oldAuthor.LastName = newAuthor.LastName;
            oldAuthor.MiddleName = newAuthor.MiddleName;

            _dbContext.Authors.Update(oldAuthor);
            _dbContext.SaveChanges();
            return newAuthor;
        }
        return new AuthorModel();
    }
}*/
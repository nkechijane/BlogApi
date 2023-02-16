using BlogAPI.Models;
using Microsoft.AspNetCore.Mvc;


namespace BlogAPI.Controllers;

[Controller]
[Route("[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorsRepository _authorsRepository;

    public AuthorsController(IAuthorsRepository authorsRepository)
    {
        _authorsRepository = authorsRepository;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var allAuthors =  _authorsRepository.GetAll();
        return Ok(allAuthors);
    }

    [HttpPost]
    public IActionResult Save(Author author)
    {
        var newlySavedAuthor = _authorsRepository.Save(author);
        return Ok(newlySavedAuthor);
    }

    [HttpGet("author/{id}")]
    public IActionResult Get([FromRoute] int id)
    {
        if (_authorsRepository.Get(id).HasValue)
        {
            return Ok(_authorsRepository.Get(id).ReadValue);
        }

        return NotFound();
    }
}

public class AuthorSaveModel
{
}

public interface IAuthorsRepository
{
    Author Save(Author author);
    IEnumerable<Author> GetAll();
    GetAuthorResult Get(int id);
}

public class GetAuthorResult
{
    public bool HasValue { get; } = false;
    public Author ReadValue { get; } = EmptyAuthor();

    private GetAuthorResult(Author author, bool hasValue)
    {
        if (hasValue)
        {
            HasValue = true;
            ReadValue = author;
        }
    }

    public static GetAuthorResult None() => new GetAuthorResult(EmptyAuthor(), false);

    public static GetAuthorResult Some(Author author) => new GetAuthorResult(author, true);

    private static Author EmptyAuthor() => new();
}

public class AuthorsRepository : IAuthorsRepository
{
    private List<Author> _allAuthors;
    public AuthorsRepository()
    {
        _allAuthors = new List<Author>()
        {
            new Author {FirstName = "Tina", MiddleName = "Doe", LastName = "Effiong"},
            new Author {FirstName = "Forbes", MiddleName = "Jeff", LastName = "Arthor"},
            new Author {FirstName = "Forbes", MiddleName = "Jeff", LastName = "Arthor"}
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
    
    public GetAuthorResult Get(int id)
    {
        if (id == 1)
        {
            return GetAuthorResult.Some(new Author
            {
                FirstName = "Forbes",
                MiddleName = "Doe",
                LastName = "Effiong"
            });
        }

        if (id == 2)
        {
            return GetAuthorResult.Some(new Author
            {
                FirstName = "Forbes",
                MiddleName = "Jeff",
                LastName = "Arthor"
            });
        }
        
        if (id == 3)
        {
            
            return GetAuthorResult.Some(new Author
            {
                FirstName = "Jeff",
                MiddleName = "Bezos",
                LastName = "Arthor"
            });
        }

        return GetAuthorResult.None();
    }
}
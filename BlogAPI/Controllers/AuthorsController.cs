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
            return Ok(_authorsRepository.Get(id).Value);
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
    public Author Value { get; } = EmptyAuthor();

    private GetAuthorResult(Author author, bool hasValue)
    {
        if (hasValue)
        {
            HasValue = true;
            Value = author;
        }
    }

    public static GetAuthorResult None() => new GetAuthorResult(EmptyAuthor(), false);

    public static GetAuthorResult Some(Author author) => new GetAuthorResult(author, true);

    private static Author EmptyAuthor() => new();
}

public class AuthorsRepository : IAuthorsRepository
{
    public Author Save(Author author)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Author> GetAll()
    {
        return new[]
        {
            new Author {FirstName = "Tina", MiddleName = "Doe", LastName = "Effiong"},
            new Author {FirstName = "Forbes", MiddleName = "Jeff", LastName = "Arthor"},
            new Author {FirstName = "Forbes", MiddleName = "Jeff", LastName = "Arthor"}

        }.ToList();
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
                FirstName = "Forbes",
                MiddleName = "Jeff",
                LastName = "Arthor"
            });
        }

        return GetAuthorResult.None();
    }
}
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
    public IActionResult Save(AuthorSaveModel author)
    {
        var newlySavedAuthor = _authorsRepository.Save(author);
        return Ok(newlySavedAuthor);
    }

    [HttpGet]
    public IActionResult Get(int id)
    {
        var response = _authorsRepository.Get(id);
        return Ok(response);
    }
}

public class AuthorSaveModel
{
}

public interface IAuthorsRepository
{
    Author Save(AuthorSaveModel author);
    IEnumerable<Author> GetAll();
    object Get(int id);
}

public class AuthorsRepository : IAuthorsRepository
{
    public Author Save(AuthorSaveModel author)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Author> GetAll()
    {
        return new[]
        {
            new Author {Id = 0, FirstName = "Tina", MiddleName = "Doe", LastName = "Effiong"},
            new Author {Id = 1, FirstName = "Forbes", MiddleName = "Jeff", LastName = "Arthor"},
            new Author {Id = 2, FirstName = "Forbes", MiddleName = "Jeff", LastName = "Arthor"}

        }.ToList();
    }

    public object Get(int id)
    {
        return new Author()
        {
            Id = 1,
            FirstName = "Forbes",
            MiddleName = "Doe",
            LastName = "Effiong"
        };
    }
}
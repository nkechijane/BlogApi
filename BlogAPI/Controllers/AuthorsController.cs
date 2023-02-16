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
    public IActionResult Save([FromBody]Author author)
    {
        var newlySavedAuthor = _authorsRepository.Save(author);
        return Ok(newlySavedAuthor);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var author = _authorsRepository.Get(id);
        return Ok(author);
    }
}
public interface IAuthorsRepository
{
    Author Save(Author author);
    IEnumerable<Author> GetAll();
    Author Get(int id);
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

    public Author Get(int id)
    {
        var author = _allAuthors.FirstOrDefault(a => a.Id == id);
        return author ?? new Author();
    }
}
using BlogAPI.Models;
using DefaultNamespace;
using Microsoft.AspNetCore.Mvc;


namespace BlogAPI.Controllers;

[Controller]
[Route("[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorsController(IAuthorRepository authorsRepository)
    {
        _authorRepository = authorsRepository;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var allAuthors =  _authorRepository.GetAll();
        return Ok(allAuthors);
    }

    [HttpPost]
    public IActionResult Save([FromBody]Author payload)
    {
        var newlySavedAuthor = _authorRepository.Save(payload);
        return Ok(newlySavedAuthor);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var author = _authorRepository.Get(id);
        return Ok(author);
    }

    [HttpPut]
    public IActionResult Update([FromBody]Author payload)
    {
        var updatedAuthor = _authorRepository.Update(payload);
        return Ok(updatedAuthor);
    }
}
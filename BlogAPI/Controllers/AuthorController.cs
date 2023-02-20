using BlogAPI.DTOs;
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
    public IActionResult Save([FromBody]SaveAuthorModel payload)
    {
        var newlySavedAuthor = _authorRepository.Save(payload);
        return Ok();
    }

    [HttpGet("{id:guid}")]
    public IActionResult Get(Guid id)
    {
        var author = _authorRepository.Get(id);
        return Ok(author);
    }

    [HttpPut]
    public IActionResult Update([FromBody]AuthorModel payload)
    {
        var updatedAuthor = _authorRepository.Update(payload);
        return Ok(updatedAuthor);
    }
}
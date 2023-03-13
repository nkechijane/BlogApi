using BlogAPI.DTOs;
using BlogAPI.Repos;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers;

[Controller]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorRepository _authorRepository;
    
    public AuthorController(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }
    
    [HttpPost]
    public IActionResult Add([FromBody]SaveAuthorModel payload)
    {
        try
        {
            var response = _authorRepository.Add(payload);
            return !string.IsNullOrEmpty(response.Result) ? Ok(response.Result) : BadRequest("Unsuccessful!");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var response = await _authorRepository.GetAll();
            return response.Any() ? Ok(response) : BadRequest("No Author found");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        try
        {
            var response = _authorRepository.Get(id);
            return !string.IsNullOrEmpty(response.Result.FirstName) ? Ok(response.Result) : BadRequest("Please check the ID and try again.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPut]
    public IActionResult Update([FromBody]AuthorModel payload)
    {
        try
        {
            var response = _authorRepository.Update(payload);
            return response.Result ? Ok("Updated Successfully!") : BadRequest("Please check the payload and try again.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
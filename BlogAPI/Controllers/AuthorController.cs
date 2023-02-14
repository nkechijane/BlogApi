using BlogAPI.Models;
using Microsoft.AspNetCore.Mvc;


namespace BlogAPI.Controllers;

[Controller]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var authors = new[]
        {
            new Author{Id = 0, FirstName = "Tina", MiddleName = "Doe", LastName = "Effiong"},
            new Author{Id = 1, FirstName = "Forbes", MiddleName = "Jeff", LastName = "Arthor"},
            new Author{Id = 2, FirstName = "Forbes", MiddleName = "Jeff", LastName = "Arthor"},
        };
        return Ok(authors);
    }
}
using Microsoft.AspNetCore.Mvc;


namespace BlogAPI.Controllers;

[Controller]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var authors = new
        {
            FirstName = "Tina",
            MiddleName = "Doe",
            LastName = "Effiong"
        };
        return Ok(authors);
    }
}
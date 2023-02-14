using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers;

[Controller]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult CheckHealth()
    {
        var myobj = new
        {
            name = "Fred",
        };
        return Ok(myobj);
    }
}
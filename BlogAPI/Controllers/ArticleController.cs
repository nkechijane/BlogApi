using BlogAPI.Models;
using BlogAPI.Repos;
using DefaultNamespace;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers;

[Controller]
[Route("[controller]")]
public class ArticleController : ControllerBase
{
    private readonly IArticleRepository _articleRepository;

    public ArticleController(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var response = _articleRepository.GetAll();
        return Ok(response);
    }

}
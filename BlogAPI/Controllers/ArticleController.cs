using BlogAPI.Models;
using BlogAPI.Repos;
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

    [HttpPost]
    public IActionResult Add([FromBody]SaveArticleModel payload)
    {
        _articleRepository.Add(payload);
        return Ok(payload);
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        var response = _articleRepository.Get(id);
        return Ok(response);
    }

    [HttpPut]
    public IActionResult Update([FromBody]ArticleModel payload)
    {
        var updatedArticle = _articleRepository.Update(payload);
        return Ok(updatedArticle);
    }
}
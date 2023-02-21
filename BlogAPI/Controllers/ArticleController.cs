using BlogAPI.DTOs;
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
        try
        {
            var response = _articleRepository.Add(payload);
            return response ? Ok("sucessful") : BadRequest();
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
            var response = _articleRepository.Get(id);
            return !string.IsNullOrEmpty(response.Body) ? Ok(response) : BadRequest("Please check the ID and try again.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    public IActionResult Update([FromBody]ArticleModel payload)
    {
        var updatedArticle = _articleRepository.Update(payload);
        return Ok(updatedArticle);
    }
}
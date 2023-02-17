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

    [HttpPost]
    public IActionResult Add([FromBody]Article payload)
    {
        var article = new Article()
        {
            Id = payload.Id, 
            Title = payload.Title,
            Body = payload.Body,
            Published = payload.Published,
            LastEdited = payload.LastEdited
        };
        _articleRepository.Add(article);
        return Ok(article);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var response = _articleRepository.Get(id);
        return Ok(response);
    }
    

}
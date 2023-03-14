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

    [HttpPost]
    public IActionResult Add([FromBody]SaveArticleModel payload)
    {
        try
        {
            var response = _articleRepository.Add(payload);
            return !string.IsNullOrEmpty(response.Result) ? Ok(response.Result) : BadRequest("Request was unsuccessful!");
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
            var response = await _articleRepository.GetAll();
            return response.Any() ? Ok(response) : Ok(new List<ArticleModel>());
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
            return !string.IsNullOrEmpty(response.Result.Body) ? Ok(response.Result) : NotFound("Please check the ID and try again.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    public IActionResult Update([FromBody]ArticleModel payload)
    {
        try
        {
            var response = _articleRepository.Update(payload);
            return response.Result ? Ok("Updated Successfully!") : NotFound("Please check the payload and try again.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
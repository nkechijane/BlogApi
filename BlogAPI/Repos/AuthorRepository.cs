using System.Text;
using BlogAPI.DTOs;
using BlogAPI.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace BlogAPI.Repos;

public class AuthorRepository : IAuthorRepository
{
    private readonly IDistributedCache _cache;

    public AuthorRepository(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<string> Add(SaveAuthorModel newAuthor)
    {
        var author = new Author()
        {
            Id = Guid.NewGuid(),
            FirstName = newAuthor.FirstName,
            MiddleName = newAuthor.MiddleName,
            LastName = newAuthor.LastName,
            RegisteredDate = DateTime.Now
        };
        await Save(author);

        return author.Id.ToString();
    }

    public Task<IEnumerable<AuthorModel>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<AuthorModel> Get(Guid id)
    {
        var author = GetAuthorById(id.ToString()).Result;
        if (!string.IsNullOrEmpty(author.FirstName))
        {
            var response = new AuthorModel()
            {
                Id = author.Id,
                FirstName = author.FirstName,
                MiddleName = author.MiddleName,
                LastName = author.LastName,
            };
            return response;
        }
        return new AuthorModel();
    }

    public Task<bool> Update(AuthorModel authorToUpdate)
    {
        throw new NotImplementedException();
    }
    
    private async Task<Author> GetAuthorById(string id)
    {
        byte[] authorsInRedis = await _cache.GetAsync(id);
        if ((authorsInRedis?.Count() ?? 0) > 0)
        {
            var authorString = Encoding.UTF8.GetString(authorsInRedis);
            var allAuthors = JsonConvert.DeserializeObject<Author>(authorString);

            return allAuthors;
        }

        return new Author();
    }
    private async Task Save(Author newAuthor)
    {
        var serializedAuthor = JsonConvert.SerializeObject(newAuthor);

        byte[] authorToCache = Encoding.UTF8.GetBytes(serializedAuthor);
        await _cache.SetAsync(newAuthor.Id.ToString(), authorToCache);
    }
}
using System.Text;
using BlogAPI.DTOs;
using BlogAPI.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace BlogAPI.Repos;

public class AuthorRepository : IAuthorRepository
{
    private readonly IDistributedCache _cache;
    private readonly BlogConfig _blogConfig;

    public AuthorRepository(IDistributedCache cache, BlogConfig blogConfig)
    {
        _cache = cache;
        _blogConfig = blogConfig;
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

    public async Task<List<AuthorModel>> GetAll()
    {
        var response = new List<AuthorModel>();
        var allAuthor = await FilterByKey("*",0,true);
        foreach (var key in allAuthor)
        {
            var author = await Get(new Guid(key));
            response.Add(author);
        }
        return response;
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

    public async Task<bool> Update(AuthorModel newAuthor)
    {
        var authorToUpdate = GetAuthorById(newAuthor.Id.ToString()).Result;
        if (string.IsNullOrEmpty(authorToUpdate.FirstName))
            return false;
        
        authorToUpdate.UpdatedOn = DateTime.Now;
        authorToUpdate.FirstName = newAuthor.FirstName;
        authorToUpdate.MiddleName = newAuthor.MiddleName;
        authorToUpdate.LastName = newAuthor.LastName;
        
        _cache.Remove(newAuthor.Id.ToString());
        await Save(authorToUpdate);
        return true;
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
    
    private async Task<List<string>> FilterByKey(string keyStr, int dbIndex = 0, bool trimRedisInstanceName = true)
    {
        var conn = await ConnectionMultiplexer.ConnectAsync(_blogConfig.REDIS_CACHE_CONN_STRING);

        var db = conn.GetDatabase(dbIndex);
        var listResult = new List<string>();

        foreach (var endPoint in conn.GetEndPoints())
        {
            var server = conn.GetServer(endPoint);
            var allkeys = server.Keys(dbIndex, keyStr);
            foreach (var key in allkeys)
            {
                if (trimRedisInstanceName)
                {
                    listResult.Add(key.ToString().Replace("master", ""));
                }
                else
                {
                    listResult.Add(key);
                }
                //var val = db.StringGet(key);
                Console.WriteLine($"key: {key}, value:");
            }
        }
        return listResult;
    }
}
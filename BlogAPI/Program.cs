using System.Configuration;
using Alachisoft.NCache.Caching.Distributed;
using BlogAPI.Context;
using BlogAPI.Repos;
using DefaultNamespace;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseInMemoryDatabase("BlogDb"));

builder.Services.AddControllers();
builder.Services.AddNCacheDistributedCache(configuration =>
{
    configuration.CacheName = "BlogClusterCache";
    configuration.EnableLogs = true;
    configuration.ExceptionsEnabled = true;
});
//builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IMemoryCache, MemoryCache>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("AzureRedisUrl");
    options.InstanceName = "master";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
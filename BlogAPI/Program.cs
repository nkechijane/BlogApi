using BlogAPI;
using BlogAPI.Repos;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .Build().Get<BlogConfig>();
builder.Services.AddSingleton(config);
builder.Services.AddControllers();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = config.REDIS_CACHE_CONN_STRING;
    options.InstanceName = "master";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
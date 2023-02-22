using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Context
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }
        
        public DbSet<Article> Articles { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}

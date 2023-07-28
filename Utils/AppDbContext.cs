using Microsoft.EntityFrameworkCore;
using webapp.Models;

namespace webapp.Utils
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<Product> Products { get; set; }
        // Add a constructor that accepts DbContextOptions
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        //public DbSet<Product> Products { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }
    }
}

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace webapp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty; // Initialize with an empty string

        public decimal Price { get; set; }
    }
    // Interface for the AppDbContext
    public interface IAppDbContext
    {
        DbSet<Product> Products { get; }
        // Add other DbSet properties and IDbSet methods here if needed.
    }

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

using Microsoft.EntityFrameworkCore;
using webapp.Models;

namespace webapp.Utils
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        // Add a constructor that accepts DbContextOptions
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        //public DbSet<Product> Products { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }
        // Override SaveChanges method to save CartItems
        public override int SaveChanges()
        {
            // Get the entities that are in Added or Modified state
            var modifiedEntities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Select(e => e.Entity)
                .ToList();

            // Loop through each modified entity
            foreach (var entity in modifiedEntities)
            {
                // If the entity is a CartItem, update its TotalPrice property before saving
                if (entity is CartItem cartItem)
                {
                    Console.WriteLine($"save cart item: {cartItem.ProductId}");
                    var product = Products.Find(cartItem.ProductId);
                    if (product != null)
                    {
                        cartItem.TotalPrice = cartItem.Quantity * product.Price;
                    }
                }
            }

            // Save the changes to the database
            return base.SaveChanges();
        }
    }
}

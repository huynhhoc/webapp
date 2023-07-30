using Microsoft.EntityFrameworkCore;
using webapp.Models;

namespace webapp.Utils
{
    // Interface for the AppDbContext
    public interface IAppDbContext
    {
        DbSet<Product> Products { get; }
        DbSet<CartItem> CartItems { get; set; }
        // Add other DbSet properties and IDbSet methods here if needed.
    }
}

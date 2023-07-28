using Microsoft.EntityFrameworkCore;
using webapp.Models;

namespace webapp.Utils
{
    // Interface for the AppDbContext
    public interface IAppDbContext
    {
        DbSet<Product> Products { get; }
        // Add other DbSet properties and IDbSet methods here if needed.
    }
}

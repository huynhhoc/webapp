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
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty; // Initialize with an empty string
        public int Quantity { get; set; } = 1; // Initialize with default quantity
        public decimal Price { get; set; }

        // New property for the image data
        //public byte[] ImageData { get; set;
        public byte[]? ImageData { get; set; }

        // Optional: You can include the image MIME type if needed
        public string? ImageMimeType { get; set; }
    }
}

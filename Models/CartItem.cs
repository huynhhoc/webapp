using System.ComponentModel.DataAnnotations;

namespace webapp.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } // Reference to the associated product

        public int Quantity { get; set; }
    }
}

using System.Collections.Generic;

namespace webapp.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using webapp.Models;
using webapp.Utils;
using System.Text.Json; // Add this namespace
using Humanizer;

namespace webapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAppDbContext _context;
        
        public HomeController(ILogger<HomeController> logger, IAppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Action method to display the list of products
        public ActionResult Index()
        {
            List<Product> products = _context.Products.ToList();
            ViewBag.CartItemCount = GetCartItemCountFromSession(); // Pass the cart item count to the view
            return View(products);
        }
        // Action method to display the ViewCart view
        public IActionResult ViewCart()
        {
            var cart = GetCartFromSession();

            // Group products by their ID
            var groupedCart = cart.GroupBy(p => p.Id)
                                  .Select(g => new Product
                                  {
                                      Id = g.Key,
                                      Name = g.First().Name,
                                      Price = g.First().Price,
                                      Quantity = g.Count() // Count the quantity for each product in the cart
                                  })
                                  .ToList();

            ViewBag.CartItemCount = groupedCart.Count; // Pass the cart item count to the view
            return View(groupedCart);
        }

        // Action method to add a product to the shopping cart
        public IActionResult AddToCart(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                AddToCartInSession(product);
                ViewBag.Message = $"Added {product.Name} to the cart.";
            }
            else
            {
                ViewBag.Message = "Product not found.";
            }

            // Set the cart in ViewBag so it can be accessed in the Index.cshtml view
            //ViewBag.Cart = GetCartFromSession();
            ViewBag.CartItemCount = GetCartItemCountFromSession(); // Pass the updated cart item count to the view

            List<Product> products = _context.Products.ToList();
            return View("Index", products);
        }

        // Action method to handle the form submission
        [HttpPost]
        public IActionResult Details(int id)
        {
            // Find the product by ID from the database
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                // If product found, redirect to the SearchResults page with product information
                return View("SearchResults", product);
            }
            else
            {
                // If product not found, redirect back to the Index page with an error message
                TempData["ErrorMessage"] = "Product not found.";
                return RedirectToAction("Index");
            }
        }
        // Action method to handle the purchase of all items in the cart
        public IActionResult Buy()
        {
            // Get the cart from the session
            var cart = GetCartFromSession();
            Console.WriteLine("Buy..........");
            // Save the cart items to the "CartItems" table in the database
            foreach (var item in cart)
            {
                var cartItem = new CartItem
                {
                    ProductId = item.Id,
                    Quantity = item.Quantity,
                    TotalPrice = item.Price * item.Quantity,
                    // You can also include the user ID if you have authentication and want to associate the cart items with the user
                    // UserId = <User ID>
                };
                Console.WriteLine($"Item: {cartItem.ProductId}");

                _context.CartItems.Add(cartItem);
            }

            // Save the changes to the database
            ((AppDbContext)_context).SaveChanges();
            // Clear the cart in the session since the purchase is complete
            ClearCartInSession();

            // Redirect to a "PurchaseSummary" view to display the details of the purchased items
            return RedirectToAction("PurchaseSummary");
        }


        private void AddToCartInSession(Product product)
        {
            var cart = GetCartFromSession();
            cart.Add(product);
            SaveCartToSession(cart); // Save the updated cart to the session
            Debug.WriteLine($"Added {product.Name} to cart. Current cart: {JsonConvert.SerializeObject(cart)}");
            Console.WriteLine($"Added {product.Name} to cart. Current cart: {JsonConvert.SerializeObject(cart)}");
        }

        private void SaveCartToSession(List<Product> cart)
        {
            var serializedCart = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString("Cart", serializedCart);
        }
        private List<Product> GetCartFromSession()
        {
            var cart = HttpContext.Session.GetString("Cart");
            return string.IsNullOrEmpty(cart) ? new List<Product>() : JsonConvert.DeserializeObject<List<Product>>(cart);
        }
        private int GetCartItemCountFromSession()
        {
            var cart = GetCartFromSession();
            return cart?.Count ?? 0;
        }
        private void ClearCartInSession()
        {
            HttpContext.Session.Remove("Cart");
        }

    }
}

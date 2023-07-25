using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using webapp.Models;

namespace webapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", Price = 10.99m },
            new Product { Id = 2, Name = "Product 2", Price = 15.49m },
            new Product { Id = 3, Name = "Product 3", Price = 7.99m },
        };

        // Action method to display the list of products
        public ActionResult Index()
        {
            return View(products);
        }

        // Action method to add a product to the shopping cart
        public ActionResult AddToCart(int id)
        {
            // In a real-world scenario, you'd handle the shopping cart logic here.
            // For simplicity, we'll just display a message for now.
            var product = products.Find(p => p.Id == id);
            if (product != null)
            {
                ViewBag.Message = $"Added {product.Name} to the cart.";
            }
            else
            {
                ViewBag.Message = "Product not found.";
            }

            return View("Index", products);
        }
    }
}
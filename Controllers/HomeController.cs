using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;
using webapp.Models;

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
            return View(products);
        }
        // Action method to display the AddToCart view
        public IActionResult AddToCartView()
        {
            return View("AddToCart");
        }
        // Action method to add a product to the shopping cart
        public ActionResult AddToCart(int id)
        {
            // In a real-world scenario, you'd handle the shopping cart logic here.
            // For simplicity, we'll just display a message for now.
            var product = _context.Products.Find(id);
            if (product != null)
            {
                AddToCartInTempData(product);
                ViewBag.Message = $"Added {product.Name} to the cart.";
            }
            else
            {
                ViewBag.Message = "Product not found.";
            }
            List<Product> products = _context.Products.ToList();
            var serializedCart = JsonConvert.SerializeObject(GetCartFromTempData());
            TempData["Cart"] = serializedCart; // Store the serialized cart

            return View("Index", products);
        }
         private List<Product> GetCartFromTempData()
        {
            var serializedCart = TempData["Cart"] as string;
            return string.IsNullOrEmpty(serializedCart) ? new List<Product>() : JsonConvert.DeserializeObject<List<Product>>(serializedCart);
        }


        private void AddToCartInTempData(Product product)
        {
            var cart = GetCartFromTempData();
            cart.Add(product);
            TempData["Cart"] = cart;
        }
    }
}

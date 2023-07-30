using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

using webapp.Models;
using webapp.Utils;

namespace webapp.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
              return _context.Products != null ? 
                          View(await _context.Products.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Products'  is null.");
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile ImageData)
        {
            // Debugging: Print the received file information
            if (ImageData != null)
            {
                Console.WriteLine($"Received Image: {ImageData.FileName}, Size: {ImageData.Length} bytes, ContentType: {ImageData.ContentType}");
            }
            else
            {
                Console.WriteLine("No image data received.");
            }

            if (ModelState.IsValid)
            {
                // Handle the uploaded image if provided
                if (ImageData != null && ImageData.Length > 0 && !string.IsNullOrEmpty(ImageData.ContentType))
                {
                    Console.WriteLine("Read the image data and store it in the ImageData property.");
                    // Read the image data and store it in the ImageData property
                    using (var memoryStream = new MemoryStream())
                    {
                        await ImageData.CopyToAsync(memoryStream);
                        product.ImageData = memoryStream.ToArray();
                        product.ImageMimeType = ImageData.ContentType;
                    }
                }

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile ImageData)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Check if a new image was provided
                    if (ImageData != null && ImageData.Length > 0 && !string.IsNullOrEmpty(ImageData.ContentType))
                    {
                        // New image provided, read the image data and store it in the product
                        using (var memoryStream = new MemoryStream())
                        {
                            await ImageData.CopyToAsync(memoryStream);
                            product.ImageData = memoryStream.ToArray();
                            product.ImageMimeType = ImageData.ContentType;
                        }
                    }
                    else
                    {
                        // New image was not provided, retrieve the existing product from the database
                        var existingProduct = await _context.Products.FindAsync(id);
                        if (existingProduct != null)
                        {
                            // Set the ImageData property to the previous value
                            product.ImageData = existingProduct.ImageData;
                            product.ImageMimeType = existingProduct.ImageMimeType;
                        }
                    }

                    // Mark the product as modified and update it in the context
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }


        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'AppDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using WebDevVue.Backend.Api.Models; 
namespace WebDevVue.Backend.Api
{
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase {
    private static List<Product> products = new List<Product> {};

    [HttpGet]
    public IActionResult  Get() {
        var products = new List<Product>
            {
                new Product { Id = 1, Name = "Smartphone", Price = 599, Category = "electronics", Image = "/images/smartphone.jpg" },
                new Product { Id = 2, Name = "Laptop", Price = 999, Category = "electronics", Image = "/images/laptop.jpg" },
                new Product { Id = 3, Name = "Jacket", Price = 89, Category = "clothing", Image = "/images/jacket.jpg" },
                new Product { Id = 4, Name = "Sneakers", Price = 120, Category = "clothing", Image = "/images/sneakers.jpg" },
                new Product { Id = 5, Name = "Blender", Price = 50, Category = "home", Image = "/images/blender.jpg" },
                new Product { Id = 6, Name = "Coffee Maker", Price = 80, Category = "home", Image = "/images/coffeemaker.jpg" },
                new Product { Id = 7, Name = "Headphones", Price = 200, Category = "electronics", Image = "/images/headphones.jpg" },
                new Product { Id = 8, Name = "T-shirt", Price = 25, Category = "clothing", Image = "/images/tshirt.jpg" },
                new Product { Id = 9, Name = "Vacuum Cleaner", Price = 150, Category = "home", Image = "/images/vacuumcleaner.jpg" },
                new Product { Id = 10, Name = "Smartwatch", Price = 250, Category = "electronics", Image = "/images/smartwatch.jpg" },
            };

            return Ok(products);
    }

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            product.Id = products.Count + 1;  // Auto-increment ID
            products.Add(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }
}
}
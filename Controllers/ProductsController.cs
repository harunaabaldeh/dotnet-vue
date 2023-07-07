using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly List<Product> _products;

        public ProductsController()
        {
            // Initialize with some dummy data for testing
            _products = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", Price = 9.99m },
            new Product { Id = 2, Name = "Product 2", Price = 19.99m },
            new Product { Id = 3, Name = "Product 3", Price = 14.99m }
        };
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return _products;
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            return product;
        }

        [HttpPost]
        public ActionResult<Product> Create(Product product)
        {
            // Assign a new ID (in a real scenario, this would be handled by a database)
            var newId = _products.Count + 1;
            product.Id = newId;

            _products.Add(product);

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Product updatedProduct)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            // Update the product properties
            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            _products.Remove(product);

            return NoContent();
        }
    }
}
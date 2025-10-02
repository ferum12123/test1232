using Asystem.Core.Entities;
using Asystem.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ProductsController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _db.Products.ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var p = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (p == null) return NotFound();
            return Ok(p);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product product)
        {
            var existing = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return NotFound();

            existing.Code = product.Code;
            existing.Name = product.Name;
            existing.ProductType = product.ProductType;
            existing.Price = product.Price;
            existing.IsActive = product.IsActive;
            existing.Description = product.Description;
            existing.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return NotFound();

            _db.Products.Remove(existing);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}

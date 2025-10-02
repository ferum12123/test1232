using Asystem.Core.Entities;
using Asystem.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public MaterialsController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _db.Materials.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var m = await _db.Materials.FirstOrDefaultAsync(x => x.Id == id);
            if (m == null) return NotFound();
            return Ok(m);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Material m)
        {
            _db.Materials.Add(m);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = m.Id }, m);
        }

        [HttpPost("{id}/receive")]
        public async Task<IActionResult> Receive(int id, [FromQuery] decimal count)
        {
            var m = await _db.Materials.FirstOrDefaultAsync(x => x.Id == id);
            if (m == null) return NotFound();
            m.OnHand += count;
            await _db.SaveChangesAsync();
            return Ok(m);
        }
    }
}

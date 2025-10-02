using Asystem.Core.Entities;
using Asystem.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormulasController : ControllerBase
    {
        private readonly AppDbContext _db;
        public FormulasController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var formulas = await _db.Formulas.ToListAsync();
            return Ok(formulas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var formula = await _db.Formulas.FirstOrDefaultAsync(f => f.Id == id);
            if (formula == null) return NotFound();
            return Ok(formula);
        }

        [HttpGet("by-product/{productType}")]
        public async Task<IActionResult> GetByProductType(string productType)
        {
            var formula = await _db.Formulas
                .Where(f => f.ProductType == productType && f.IsActive)
                .OrderByDescending(f => f.UpdatedAt)
                .FirstOrDefaultAsync();
            
            if (formula == null) return NotFound();
            return Ok(formula);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Formula formula)
        {
            formula.CreatedAt = DateTime.UtcNow;
            formula.UpdatedAt = DateTime.UtcNow;
            
            _db.Formulas.Add(formula);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = formula.Id }, formula);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Formula formula)
        {
            var existingFormula = await _db.Formulas.FirstOrDefaultAsync(f => f.Id == id);
            if (existingFormula == null) return NotFound();

            existingFormula.Name = formula.Name;
            existingFormula.BasePrice = formula.BasePrice;
            existingFormula.PaperMultiplierFormula = formula.PaperMultiplierFormula;
            existingFormula.PrintMultiplierFormula = formula.PrintMultiplierFormula;
            existingFormula.SizeMultiplierFormula = formula.SizeMultiplierFormula;
            existingFormula.VolumeDiscountFormula = formula.VolumeDiscountFormula;
            existingFormula.FinalFormula = formula.FinalFormula;
            existingFormula.IsActive = formula.IsActive;
            existingFormula.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return Ok(existingFormula);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var formula = await _db.Formulas.FirstOrDefaultAsync(f => f.Id == id);
            if (formula == null) return NotFound();

            _db.Formulas.Remove(formula);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("{id}/activate")]
        public async Task<IActionResult> Activate(int id)
        {
            var formula = await _db.Formulas.FirstOrDefaultAsync(f => f.Id == id);
            if (formula == null) return NotFound();

            formula.IsActive = true;
            formula.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return Ok(formula);
        }

        [HttpPost("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(int id)
        {
            var formula = await _db.Formulas.FirstOrDefaultAsync(f => f.Id == id);
            if (formula == null) return NotFound();

            formula.IsActive = false;
            formula.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return Ok(formula);
        }
    }
}

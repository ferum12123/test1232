using Asystem.Core.Entities;
using Asystem.Core.Entities.Enums;
using Asystem.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _db;
        public OrdersController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _db.Orders.Include(o => o.Tasks).ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _db.Orders.Include(o => o.Tasks).FirstOrDefaultAsync(o => o.Id == id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = order.Id }, order);
        }

        [HttpPost("{id}/stage")]
        public async Task<IActionResult> ChangeStage(int id, [FromBody] OrderStage newStage)
        {
            var order = await _db.Orders.Include(o => o.Tasks).FirstOrDefaultAsync(o => o.Id == id);
            if (order == null) return NotFound();
            order.Stage = newStage;
            await _db.SaveChangesAsync();
            return Ok(order);
        }

        [HttpPost("{id}/task/{taskId}/complete")]
        public async Task<IActionResult> CompleteTask(int id, int taskId)
        {
            var task = await _db.OrderTasks.FirstOrDefaultAsync(t => t.Id == taskId && t.OrderId == id);
            if (task == null) return NotFound();
            task.IsCompleted = true;
            await _db.SaveChangesAsync();
            return Ok(task);
        }
    }
}

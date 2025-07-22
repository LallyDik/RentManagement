using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentManagementAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Payments?tenantId=5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments([FromQuery] int tenantId)
        {
            return await _context.Payments
                .Include(p => p.Tenant)
                .Where(p => p.TenantId == tenantId)
                .ToListAsync();
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            var payment = await _context.Payments.Include(p => p.Tenant).FirstOrDefaultAsync(p => p.Id == id);
            if (payment == null)
                return NotFound();
            return payment;
        }

        // POST: api/Payments
        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPayment), new { id = payment.Id }, payment);
        }

        // PUT: api/Payments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, Payment payment)
        {
            if (id != payment.Id)
                return BadRequest();
            _context.Entry(payment).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Payments.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
                return NotFound();
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

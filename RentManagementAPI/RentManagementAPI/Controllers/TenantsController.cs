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
    public class TenantsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TenantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Tenants?userId=5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tenant>>> GetTenants([FromQuery] int userId)
        {
            return await _context.Tenants
                .Include(t => t.ResponsibleUser)
                .Include(t => t.Payments)
                .Where(t => t.ResponsibleUserId == userId)
                .ToListAsync();
        }

        // GET: api/Tenants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tenant>> GetTenant(int id)
        {
            var tenant = await _context.Tenants.Include(t => t.ResponsibleUser).Include(t => t.Payments).FirstOrDefaultAsync(t => t.Id == id);
            if (tenant == null)
                return NotFound();
            return tenant;
        }

        // POST: api/Tenants
        [HttpPost]
        public async Task<ActionResult<Tenant>> PostTenant(Tenant tenant)
        {
            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTenant), new { id = tenant.Id }, tenant);
        }

        // PUT: api/Tenants/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTenant(int id, Tenant tenant)
        {
            if (id != tenant.Id)
                return BadRequest();
            _context.Entry(tenant).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tenants.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        // DELETE: api/Tenants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTenant(int id)
        {
            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant == null)
                return NotFound();
            _context.Tenants.Remove(tenant);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

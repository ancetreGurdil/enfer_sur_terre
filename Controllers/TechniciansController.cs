using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InterventionAPI.Data;
using InterventionAPI.Models;

namespace InterventionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechniciansController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TechniciansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Technicians
        [Authorize(Roles = "Admin,Technicien")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Technician>>> GetTechnicians()
        {
            var technicians = await _context.Technicians.ToListAsync();
            return Ok(technicians);
        }

        // GET: api/Technicians/5
        [Authorize(Roles = "Admin,Technician")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Technician>> GetTechnician(int id)
        {
            var technician = await _context.Technicians.FindAsync(id);

            if (technician == null)
            {
                return NotFound();
            }

            return Ok(technician);
        }

        // POST: api/Technicians
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Technician>> PostTechnician(Technician technician)
        {
            _context.Technicians.Add(technician);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTechnician", new { id = technician.Id }, technician);
        }

        // PUT: api/Technicians/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTechnician(int id, Technician technician)
        {
            if (id != technician.Id)
            {
                return BadRequest();
            }

            _context.Entry(technician).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TechnicianExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Technicians/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTechnician(int id)
        {
            var technician = await _context.Technicians.FindAsync(id);
            if (technician == null)
            {
                return NotFound();
            }

            _context.Technicians.Remove(technician);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TechnicianExists(int id)
        {
            return _context.Technicians.Any(e => e.Id == id);
        }
    }
}

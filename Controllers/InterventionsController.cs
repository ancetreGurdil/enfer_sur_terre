using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InterventionAPI.Data;
using InterventionAPI.Models;

namespace InterventionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterventionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InterventionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Interventions
        [Authorize(Roles = "Admin,Technicien")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Intervention>>> GetInterventions()
        {
            var interventions = await _context.Interventions
                .Include(i => i.Client)
                .Include(i => i.ServiceType)
                .Include(i => i.TechniciansInterventions)
                    .ThenInclude(t => t.Technician)
                .Include(i => i.InterventionMaterials)
                    .ThenInclude(m => m.Material)
                .ToListAsync();
            return Ok(interventions);
        }

        // GET: api/Interventions/5
        [Authorize(Roles = "Admin,Technicien")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Intervention>> GetIntervention(int id)
        {
            var intervention = await _context.Interventions
                .Include(i => i.Client)
                .Include(i => i.ServiceType)
                .Include(i => i.TechniciansInterventions)
                    .ThenInclude(t => t.Technician)
                .Include(i => i.InterventionMaterials)
                    .ThenInclude(m => m.Material)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (intervention == null)
            {
                return NotFound();
            }

            return Ok(intervention);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<InterventionResponseDto>> PostIntervention(InterventionDto dto)
        {
            var intervention = new Intervention
            {
                ScheduledDate = dto.ScheduledDate,
                Description = dto.Description,
                ClientId = dto.ClientId,
                ServiceTypeId = dto.ServiceTypeId,
                TechniciansInterventions = dto.TechnicianIds?.Select(tid => new TechnicianIntervention
                {
                    TechnicianId = tid
                }).ToList(),
                InterventionMaterials = dto.MaterialIds?.Select(mid => new InterventionMaterial
                {
                    MaterialId = mid
                }).ToList()
            };

            _context.Interventions.Add(intervention);
            await _context.SaveChangesAsync();

            // Récupération des noms après sauvegarde (join avec les entités liées)
            var response = new InterventionResponseDto
            {
                ClientId = intervention.ClientId,
                ServiceTypeName = await _context.ServiceTypes
                                                .Where(st => st.Id == intervention.ServiceTypeId)
                                                .Select(st => st.Name)
                                                .FirstOrDefaultAsync(),
                TechnicianNames = await _context.Technicians
                                                .Where(t => dto.TechnicianIds.Contains(t.Id))
                                                .Select(t => t.Name)
                                                .ToListAsync(),
                MaterialNames = await _context.Materials
                                            .Where(m => dto.MaterialIds.Contains(m.Id))
                                            .Select(m => m.Name)
                                            .ToListAsync()
            };

            return Ok(response);
        }



        // PUT: api/Interventions/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIntervention(int id, Intervention intervention)
        {
            if (id != intervention.Id)
            {
                return BadRequest();
            }

            _context.Entry(intervention).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterventionExists(id))
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

        // DELETE: api/Interventions/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIntervention(int id)
        {
            var intervention = await _context.Interventions.FindAsync(id);
            if (intervention == null)
            {
                return NotFound();
            }

            _context.Interventions.Remove(intervention);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InterventionExists(int id)
        {
            return _context.Interventions.Any(e => e.Id == id);
        }
    }
}

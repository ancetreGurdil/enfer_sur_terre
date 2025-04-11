using InterventionAPI.Models;
using InterventionAPI.Data;
using Microsoft.EntityFrameworkCore;  // Ajouter ceci


namespace InterventionAPI.Repositories
{
    public interface ITechnicianRepository
    {
        Task<IEnumerable<Technician>> GetTechniciansAsync();
        Task<Technician> GetTechnicianByIdAsync(int id);
        Task AddTechnicianAsync(Technician technician);
        Task SaveAsync();
    }

    public class TechnicianRepository : ITechnicianRepository
    {
        private readonly ApplicationDbContext _context;

        public TechnicianRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Technician>> GetTechniciansAsync()
        {
            return await _context.Technicians.ToListAsync();
        }

        public async Task<Technician> GetTechnicianByIdAsync(int id)
        {
            return await _context.Technicians.FindAsync(id);
        }

        public async Task AddTechnicianAsync(Technician technician)
        {
            _context.Technicians.Add(technician);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

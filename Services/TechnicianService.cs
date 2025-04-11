using Microsoft.EntityFrameworkCore;  
using InterventionAPI.Models;
using InterventionAPI.Data;

namespace InterventionAPI.Services

{
    public class TechnicianService
    {
        private readonly ApplicationDbContext _context;

        public TechnicianService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Technician>> GetAllTechniciansAsync()
        {
            return await _context.Technicians.ToListAsync();
        }

        public async Task<Technician> GetTechnicianByIdAsync(int id)
        {
            return await _context.Technicians.FindAsync(id);
        }

        // D'autres méthodes pour créer, mettre à jour, supprimer les Technicians...
    }
}

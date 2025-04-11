using Microsoft.AspNetCore.Identity;

namespace InterventionAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Si tu veux ajouter des propriétés spécifiques à l'utilisateur (admin, technicien, etc.)
        public string FullName { get; set; }
        
    }
}

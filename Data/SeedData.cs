using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using InterventionAPI.Models;

namespace InterventionAPI.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // Créer la base de données si elle n'existe pas
            context.Database.EnsureCreated();

            // Seed des rôles (Admin, Technicien)
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!await roleManager.RoleExistsAsync("Technicien"))
            {
                await roleManager.CreateAsync(new IdentityRole("Technicien"));
            }

            // Seed d'un utilisateur Admin si aucun utilisateur n'existe
            if (await userManager.FindByEmailAsync("admin@intervention.com") == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@intervention.com",
                    Email = "admin@intervention.com",
                    FullName = "Admin User"
                };
                var result = await userManager.CreateAsync(adminUser, "Admeazeazezazeezain1234!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Seed des clients
            if (!context.Clients.Any())
            {
                context.Clients.AddRange(
                    new Client { Name = "Client 1", Address = "123 Main St", PhoneNumber = "1234567890" },
                    new Client { Name = "Client 2", Address = "456 Oak St", PhoneNumber = "0987654321" }
                );
                await context.SaveChangesAsync();
            }

            // Seed des techniciens
            if (!context.Technicians.Any())
            {
                context.Technicians.AddRange(
                    new Technician { Name = "Technicien 1", Specialty = "Chauffage" },
                    new Technician { Name = "Technicien 2", Specialty = "Électricité" }
                );
                await context.SaveChangesAsync();
            }

            // Seed des types de service
            if (!context.ServiceTypes.Any())
            {
                context.ServiceTypes.AddRange(
                    new ServiceType { Name = "Chauffage" },
                    new ServiceType { Name = "Électricité" },
                    new ServiceType { Name = "Plomberie" }
                );
                await context.SaveChangesAsync();
            }

            // Seed des matériaux
            if (!context.Materials.Any())
            {
                context.Materials.AddRange(
                    new Material { Name = "Chaudière", Description = "Chaudière à gaz" },
                    new Material { Name = "Fil électrique", Description = "Fil de câble électrique" }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}

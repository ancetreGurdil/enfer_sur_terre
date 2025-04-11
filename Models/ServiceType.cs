using System.ComponentModel.DataAnnotations;

namespace InterventionAPI.Models

{
    public class ServiceType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }  // Chauffage, réseau, électricité, etc.
    }
}

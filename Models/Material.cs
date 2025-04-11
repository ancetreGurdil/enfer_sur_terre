using System.ComponentModel.DataAnnotations;

namespace InterventionAPI.Models
{
    public class Material
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

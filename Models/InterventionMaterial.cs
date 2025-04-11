using System.ComponentModel.DataAnnotations;

namespace InterventionAPI.Models
{
    public class InterventionMaterial
    {
        [Key]
        public int MaterialId { get; set; }
        public Material Material { get; set; }

        public int InterventionId { get; set; }
        public Intervention Intervention { get; set; }
    }
}

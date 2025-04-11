using System.ComponentModel.DataAnnotations;

namespace InterventionAPI.Models
{
    public class TechnicianIntervention
    {
        [Key]
        public int TechnicianId { get; set; }
        public Technician Technician { get; set; }

        public int InterventionId { get; set; }
        public Intervention Intervention { get; set; }
    }
}

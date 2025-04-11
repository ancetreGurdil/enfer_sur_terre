using System.ComponentModel.DataAnnotations;

namespace InterventionAPI.Models
{
    public class Technician
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }  // Par exemple, plomberie, électricité, etc.
    }
}

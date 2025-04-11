using System;  // Pour DateTime
using System.Collections.Generic;  // Pour List<T>
using System.ComponentModel.DataAnnotations;


namespace InterventionAPI.Models
{
    public class Intervention
    {
        [Key]
        public int Id { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string Description { get; set; }
        public int ClientId { get; set; }
        public int ServiceTypeId { get; set; }
        public List<int> TechnicianIds { get; set; }
        public List<int> MaterialIds { get; set; }

    }
}

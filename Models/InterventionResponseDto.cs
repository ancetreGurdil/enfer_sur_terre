namespace InterventionAPI.Models
{
public class InterventionResponseDto
{
    public int ClientId { get; set; }
    public string ServiceTypeName { get; set; }
    public List<string> TechnicianNames { get; set; }
    public List<string> MaterialNames { get; set; }
}
}
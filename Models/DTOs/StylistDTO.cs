namespace HillarysHairCare.Models.DTOs;

public class StylistDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsActive { get; set; }
    public List<AppointmentDTO>? Appointments { get; set; }
}

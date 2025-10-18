namespace HillarysHairCare.Models;

public class Service
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public List<AppointmentService> AppointmentServices { get; set; }
}

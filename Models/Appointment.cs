namespace HillarysHairCare.Models;

public class Appointment
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public int StylistId { get; set; }
    public Stylist Stylist { get; set; }
    public DateTime AppointmentDate { get; set; }
    public bool IsCanceled { get; set; }
    public List<AppointmentService> AppointmentServices { get; set; }
}

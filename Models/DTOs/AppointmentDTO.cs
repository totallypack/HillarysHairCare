namespace HillarysHairCare.Models.DTOs;

public class AppointmentDTO
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public CustomerDTO? Customer { get; set; }
    public int StylistId { get; set; }
    public StylistDTO? Stylist { get; set; }
    public DateTime AppointmentDate { get; set; }
    public bool IsCanceled { get; set; }
    public List<ServiceDTO>? Services { get; set; }
    public decimal TotalCost
    {
        get
        {
            return Services?.Sum(s => s.Price) ?? 0;
        }
    }
}

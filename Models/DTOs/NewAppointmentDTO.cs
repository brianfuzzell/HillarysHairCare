namespace HillarysHairCare.Models.DTOs;

public class NewAppointmentDTO
{
    public int CustomerId { get; set; }
    public int StylistId { get; set; }
    public DateTime? AppointmentTime { get; set; }
    public List<int> ServiceIds { get; set; }
}

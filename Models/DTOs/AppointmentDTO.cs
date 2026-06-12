using System.ComponentModel.DataAnnotations;

namespace HillarysHairCare.Models.DTOs;

public class AppointmentDTO
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public CustomerDTO Customer { get; set; }
    public int StylistId { get; set; }
    public StylistDTO Stylist { get; set; }
    public DateTime? AppointmentTime { get; set; }
    public bool IsCancelled { get; set; }
    public List<AppointmentServiceDTO> AppointmentServices { get; set; }
    public decimal TotalCost { get; set; }
}
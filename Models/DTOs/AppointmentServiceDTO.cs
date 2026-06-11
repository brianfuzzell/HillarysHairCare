using System.ComponentModel.DataAnnotations;

namespace HillarysHairCare.Models.DTOs;

public class AppointmentServiceDTO
{
    public int Id { get; set; }
    public int AppointmentId { get; set; }
    public Appointment Appointment { get; set; }
    public int ServiceId { get; set; }
    public Service Service { get; set; }
}
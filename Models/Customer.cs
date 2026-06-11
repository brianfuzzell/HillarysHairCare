using System.ComponentModel.DataAnnotations;

namespace HillarysHairCare.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public List<Appointment> Appointments { get; set; }
}
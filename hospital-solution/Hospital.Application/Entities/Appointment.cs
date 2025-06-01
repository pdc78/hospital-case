using System.ComponentModel.DataAnnotations;

namespace Hospital.Application.Entities;

public class Appointment
{
    public int Id { get; set; }
    [Required]
    public required string Cpr { get; set; }
    [Required]
    public required string PatientName { get; set; }
    [Required]
    public required DateTime AppointmentDate { get; set; }
    [Required]
    public required string Department { get; set; }
    [Required]
    public required string DoctorName { get; set; }
}

namespace Hospital.Application.Entities;

public class AppointmentDto
{

    public string Cpr { get; set; } = string.Empty;
    public string PatientName { get; set; } = string.Empty;
    public DateTime AppointmentDate { get; set; }
    public string Department { get; set; } = string.Empty;
    public string DoctorName { get; set; } = string.Empty;

    public AppointmentDto() { }

    public AppointmentDto(string cpr, string patientName, DateTime appointmentDate, string department, string doctorName)
    {
        Cpr = cpr;
        PatientName = patientName;
        AppointmentDate = appointmentDate;
        Department = department;
        DoctorName = doctorName;
    }
}
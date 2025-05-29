using System.Runtime.CompilerServices;

namespace Hospital.Application.Entities;

public class AppointmentDto
{
    public string Cpr { get; set; }

    public string PatientName { get; set; }

    public DateTime AppointmentDate { get; set; }

    public string Department { get; set; }

    public string DoctorName { get; set; }

    public AppointmentDto(string cpr, string patientName, DateTime appointmentDate, string department, string doctorName)
    {
        Cpr = cpr;
        PatientName = patientName;
        AppointmentDate = appointmentDate;
        Department = department;
        DoctorName = doctorName;
    }
}
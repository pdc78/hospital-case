namespace Hospital.Application.Services.Interfaces;
public interface IAppointmentService
{
    Task<bool> ScheduleAppointment(
        string cpr, 
        string patientName, 
        DateTime appointmentDate,
        string department, 
        string doctorName);
}
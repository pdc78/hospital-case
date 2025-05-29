using Hospital.Application.Entities;
using Hospital.Application.Factories;
using Hospital.Application.Repositories;
using Hospital.Application.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Services;

public class AppointmentService(AppointmentRepository appointmentRepository, IDepartmentValidatorFactory departmentValidatorFactory, ILogger<AppointmentService> logger) : IAppointmentService
{
    private readonly AppointmentRepository _repository = appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
    private readonly IDepartmentValidatorFactory _validatorFactory = departmentValidatorFactory ?? throw new ArgumentNullException(nameof(departmentValidatorFactory));
    private readonly ILogger<AppointmentService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<bool> ScheduleAppointment(
        string cpr, string patientName, DateTime appointmentDate,
        string department, string doctorName)
    {
        // Basic validation
        if (!IsValidRequest(cpr, department, doctorName, appointmentDate))
        {
            _logger.LogError("Invalid appointment request.");
            return false;
        }

        // Validate CPR before scheduling
        if (!await new NationalRegistryService().ValidateCpr(cpr))
        {
            _logger.LogError("Invalid CPR number. Cannot schedule appointment.");
            return false;
        }

        _logger.LogInformation($"Scheduling appointment for {patientName} (CPR: {cpr}) in {department} with {doctorName} on {appointmentDate}");


        var validator = _validatorFactory.GetValidator(department);
        if (validator == null)
        {
            _logger.LogError($"Unsupported department: {department}");
            return false;
        }

        var (isValid, errorMessage) = await validator.ValidateAsync(
            new AppointmentDto(cpr, patientName, appointmentDate, department, doctorName)
        );
        
        if (!isValid)
        {
            _logger.LogError($"[ERROR] {errorMessage}");
            return false;
        }

        await _repository.AddAsync(new Appointment
        {
            Cpr = cpr,
            PatientName = patientName,
            AppointmentDate = appointmentDate,
            Department = department,
            DoctorName = doctorName
        });

        _logger.LogInformation($"Appointment successfully scheduled for {patientName} (CPR: {cpr})");
        return true;
    }
    private static bool IsValidRequest(string cpr, string department, string doctorName, DateTime appointmentDate)
    {
        return !string.IsNullOrEmpty(cpr)
            && !string.IsNullOrEmpty(department)
            && !string.IsNullOrEmpty(doctorName)
            && appointmentDate >= DateTime.Now;
    }
}
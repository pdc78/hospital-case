using Hospital.Application.Entities;

namespace Hospital.Application.Validators.Interfaces;

public interface IDepartmentValidator
{
    Task<(bool IsValid, string? ErrorMessage)> ValidateAsync(AppointmentDto appointmentDto);
}

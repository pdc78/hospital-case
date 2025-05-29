using Hospital.Application.Entities;

namespace Hospital.Application.Rules.Interfaces;

public interface IValidationRule
{
    Task<(bool IsValid, string? ErrorMessage)> ValidateAsync(AppointmentDto appointmentDto);
}
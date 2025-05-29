using Hospital.Application.Entities;
using Hospital.Application.Rules.Interfaces;

namespace Hospital.Application.Rules;

public class AssignedToGpRule : IValidationRule
{
    public async Task<(bool IsValid, string? ErrorMessage)> ValidateAsync(AppointmentDto appointmentDto)
    {
        if (appointmentDto == null)
        {
            throw new ArgumentNullException(nameof(appointmentDto));
        }
        return await Task.FromResult<(bool, string?)>(
                    IsAssignedToGP(appointmentDto.Cpr, appointmentDto.DoctorName) ? (true, null) : (false, $"Patients can only book appointments with their assigned GP."));
    }

    private bool IsAssignedToGP(string cpr, string doctorName)
    {
        Console.WriteLine($"[LOG] Checking GP assignment for {cpr}");
        return true;
    }
}

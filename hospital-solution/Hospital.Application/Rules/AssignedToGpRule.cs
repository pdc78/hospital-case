using Hospital.Application.Rules.Interfaces;

namespace Hospital.Application.Rules;
public class AssignedToGpRule : IValidationRule
{
    public Task<(bool IsValid, string? ErrorMessage)> ValidateAsync(string cpr, string doctorName)
    {

        return Task.FromResult<(bool, string?)>(
            IsAssignedToGP(cpr,doctorName) ? (true, null) : (false, $"Patients can only book appointments with their assigned GP."));
    }

    private bool IsAssignedToGP(string cpr, string doctorName)
    {
        Console.WriteLine($"[LOG] Checking GP assignment for {cpr}");
        return true; 
    }
}

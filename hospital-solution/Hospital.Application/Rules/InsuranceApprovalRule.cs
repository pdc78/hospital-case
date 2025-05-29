using Hospital.Application.Entities;
using Hospital.Application.Rules.Interfaces;

namespace Hospital.Application.Rules;
public class InsuranceApprovalRule : IValidationRule
{
    private readonly IEnumerable<string> _departments;

    public InsuranceApprovalRule(List<string> departments)
    {
        _departments = departments;
    }

    public async Task<(bool IsValid, string? ErrorMessage)> ValidateAsync(AppointmentDto appointmentDto)
    {
        if (appointmentDto == null)
        {
            throw new ArgumentNullException(nameof(appointmentDto));
        }

        var hasInsuranceApproval = RequiresInsuranceApproval(appointmentDto.Department) && HasValidInsuranceApproval(appointmentDto.Cpr,appointmentDto.Department);

        return await Task.FromResult<(bool, string?)>(
            hasInsuranceApproval ? (true, null) : (false, $"{appointmentDto.Department} requires a insurance approval."));
    }
  

    private bool HasValidInsuranceApproval(string cpr, string department)
    {
        Console.WriteLine($"[LOG] Checking if insurance approval exists for CPR {cpr} in {department}");
        return true; 
    }

    private bool RequiresInsuranceApproval(string department)
    {
        return _departments.Any(a => a.Equals(department));
    }
}
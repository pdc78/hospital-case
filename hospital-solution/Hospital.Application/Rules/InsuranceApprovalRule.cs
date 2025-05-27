using Hospital.Application.Rules.Interfaces;

namespace Hospital.Application.Rules;
public class InsuranceApprovalRule : IValidationRule
{
    private readonly IEnumerable<string> _departments;

    public InsuranceApprovalRule(List<string> departments)
    {
        _departments = departments;
    }

    public Task<(bool IsValid, string? ErrorMessage)> ValidateAsync(string cpr, string department)
    { 
        var hasInsuranceApproval = RequiresInsuranceApproval(department) && HasValidInsuranceApproval(cpr, department);

        return Task.FromResult<(bool, string?)>(
            hasInsuranceApproval ? (true, null) : (false, $"{department} requires a insurance approval."));
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
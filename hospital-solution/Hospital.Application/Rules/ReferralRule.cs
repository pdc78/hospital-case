using Hospital.Application.Rules.Interfaces;

namespace Hospital.Application.Rules;
public class ReferralRule : IValidationRule
{
    private readonly IEnumerable<string> _departments;
    public ReferralRule(IEnumerable<string> departments)
    {
        _departments = departments;
    }

    public Task<(bool, string?)> ValidateAsync(string cpr, string department)
    {
        bool hasReferral = RequiresReferral(department) && HasValidReferral(cpr, department);

        return Task.FromResult<(bool, string?)>(
            hasReferral ? (true, null) : (false, $"{department} requires a valid referral."));
    }
    
    private bool RequiresReferral(string department)
    {
        return _departments.Any(a=>a.Equals(department));
    }
    
    private bool HasValidReferral(string cpr, string department)
    {
        Console.WriteLine($"[LOG] Checking if referral exists for CPR {cpr} in {department}");
        return true; // Dummy check (To be replaced later)
    }
}

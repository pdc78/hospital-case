using Hospital.Application.Entities;
using Hospital.Application.Rules.Interfaces;

namespace Hospital.Application.Rules;
public class ReferralRule : IValidationRule
{
    private readonly IEnumerable<string> _departments;
    public ReferralRule(IEnumerable<string> departments)
    {
        _departments = departments;
    }

    public Task<(bool, string?)> ValidateAsync(AppointmentDto appointmentDto)
    {
        if (appointmentDto == null)
        {
            throw new ArgumentNullException(nameof(appointmentDto));
        }

        bool hasReferral = RequiresReferral(appointmentDto.Department) && HasValidReferral(appointmentDto.Cpr, appointmentDto.Department);

        return Task.FromResult<(bool, string?)>(
            hasReferral ? (true, null) : (false, $"{appointmentDto.Department} requires a valid referral."));
    }
      
    
    private bool RequiresReferral(string department)
    {
        return _departments.Any(a=>a.Equals(department));
    }
    
    private bool HasValidReferral(string cpr, string department)
    {
        Console.WriteLine($"[LOG] Checking if referral exists for CPR {cpr} in {department}");
        return true;
    }
}

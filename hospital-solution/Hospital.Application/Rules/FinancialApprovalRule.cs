using Hospital.Application.Entities;
using Hospital.Application.Rules.Interfaces;

namespace Hospital.Application.Rules
{
    public class FinancialApprovalRule : IValidationRule
    {
        private IEnumerable<string> _departments;
        public FinancialApprovalRule(IEnumerable<string> departments)
        {
            _departments = departments;
        }
        public Task<(bool IsValid, string? ErrorMessage)> ValidateAsync(AppointmentDto appointmentDto)
        {
            if (appointmentDto == null)
            {
                throw new ArgumentNullException(nameof(appointmentDto));
            }
            var hasFinancialApproval = RequiresFinancialApproval(appointmentDto.Department) && HasValidFinancialApproval(appointmentDto.Cpr, appointmentDto.Department);
            return Task.FromResult(hasFinancialApproval ? (true, (string?)null) : (false, $"{appointmentDto.Department} requires a financial approval."));
        }


        private bool HasValidFinancialApproval(string cpr, string department)
        {
            Console.WriteLine($"[LOG] Checking if financial approval exists for CPR {cpr} in {department}");
            return true; // Dummy check (To be replaced later)
        }

        private bool RequiresFinancialApproval(string department)
        {
            return _departments.Any(a => a.Equals(department));
        }
    }
}

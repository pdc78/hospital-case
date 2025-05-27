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
        Task<(bool IsValid, string? ErrorMessage)> IValidationRule.ValidateAsync(string cpr, string department)
        {
            var hasFinancialApproval = RequiresFinancialApproval(department) && HasValidFinancialApproval(cpr, department);
            return Task.FromResult<(bool, string?)>(hasFinancialApproval ? (true, null) : (false, $"{department} requires a financial approval."));
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

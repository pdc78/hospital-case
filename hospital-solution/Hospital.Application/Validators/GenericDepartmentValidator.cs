using Hospital.Application.Rules.Interfaces;
using Hospital.Application.Validators.Interfaces;

namespace Hospital.Application.Validators
{
    public class GenericDepartmentValidator : IDepartmentValidator
    {
        private readonly string _department;
        private readonly IEnumerable<IValidationRule> _rules;

        public GenericDepartmentValidator(string department, IEnumerable<IValidationRule> rules)
        {
            _department = department;
            _rules = rules;
        }
        public async Task<(bool, string?)> ValidateAsync(string cpr, string doctorName)
        {
            foreach (var rule in _rules)
            {
                var (isValid, errorMessage) = await rule.ValidateAsync(cpr, _department);
                if (!isValid)
                    return (false, errorMessage);
            }

            return (true, null);
        }
    }
}

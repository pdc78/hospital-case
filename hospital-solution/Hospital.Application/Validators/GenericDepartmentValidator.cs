using Hospital.Application.Entities;
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
       
        public async Task<(bool IsValid, string? ErrorMessage)> ValidateAsync(AppointmentDto appointmentDto)
        {
              foreach (var rule in _rules)
            {
                var (isValid, errorMessage) = await rule.ValidateAsync(appointmentDto);
                if (!isValid)
                    return (false, errorMessage);
            }

            return (true, null);
        }
    }
}

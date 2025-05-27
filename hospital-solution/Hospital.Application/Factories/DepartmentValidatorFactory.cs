using Hospital.Application.Rules.Interfaces;
using Hospital.Application.Rules;
using Hospital.Application.Validators.Interfaces;
using Hospital.Application.Validators;
using Microsoft.Extensions.DependencyInjection;

public class DepartmentValidatorFactory
{
    private readonly Dictionary<string, IDepartmentValidator> _validators;

    public DepartmentValidatorFactory(IServiceProvider provider)
    {
        _validators = new Dictionary<string, IDepartmentValidator>(StringComparer.OrdinalIgnoreCase)
        {
            ["GeneralPractice"]
            = new GenericDepartmentValidator("GeneralPractice", new IValidationRule[]
            {
                provider.GetRequiredService<AssignedToGpRule>()
            })
            ,
            ["Physiotherapy"] = new GenericDepartmentValidator("Physiotherapy", new IValidationRule[]
            {
                provider.GetRequiredService<ReferralRule>(),
                provider.GetRequiredService<InsuranceApprovalRule>()
            }),
            ["Surgery"] = new GenericDepartmentValidator("Surgery", new IValidationRule[]
            {
                provider.GetRequiredService<ReferralRule>()
            }),
            ["Radiology"] = new GenericDepartmentValidator("Radiology", new IValidationRule[]
            {
                provider.GetRequiredService<ReferralRule>(),
                provider.GetRequiredService<FinancialApprovalRule>()
            })
        };
    }

    public IDepartmentValidator? GetValidator(string department)
    {
        _validators.TryGetValue(department, out var validator);
        return validator;
    }
}

using Hospital.Application.Rules.Interfaces;
using Hospital.Application.Validators.Interfaces;
using Hospital.Application.Validators;
using Microsoft.Extensions.DependencyInjection;
using Hospital.Application.Factories;
using Hospital.Application.Rules.Configurations;
using Microsoft.Extensions.Options;
using System.Reflection;

public class DepartmentValidatorFactory : IDepartmentValidatorFactory
{
    private readonly Dictionary<string, IDepartmentValidator> _validators;

    public DepartmentValidatorFactory(
        IServiceProvider provider,
        IOptions<DepartmentRulesConfiguration> config)
    {
        var ruleMapping = config.Value;

        _validators = new Dictionary<string, IDepartmentValidator>(StringComparer.OrdinalIgnoreCase);

        foreach (var kvp in ruleMapping)
        {
            var departmentName = kvp.Key;
            var ruleTypeNames = kvp.Value;

            var rules = new List<IValidationRule>();

            foreach (var typeName in ruleTypeNames)
            {
                var ruleType = Assembly.GetExecutingAssembly()
                                       .GetTypes()
                                       .FirstOrDefault(t => t.Name == typeName && typeof(IValidationRule).IsAssignableFrom(t));

                if (ruleType == null)
                    throw new InvalidOperationException($"Rule type '{typeName}' not found.");

                var ruleInstance = (IValidationRule)provider.GetRequiredService(ruleType);
                rules.Add(ruleInstance);
            }

            _validators[departmentName] = new GenericDepartmentValidator(departmentName, rules);
        }
    }

    public IDepartmentValidator? GetValidator(string department)
    {
        _validators.TryGetValue(department, out var validator);
        return validator;
    }
}

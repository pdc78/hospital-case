using Hospital.Application.Rules;
using Hospital.Application.Rules.Interfaces;
using Hospital.WebApi.Configurations;
using Microsoft.Extensions.Options;

namespace Hospital.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddValidationRules(this IServiceCollection services)
    {
        services.AddSingleton(provider =>
        {
            var config = provider.GetRequiredService<IOptions<DepartmentConfigurations>>().Value;
            return new ReferralRule(config.ReferralDepartments);
        });

        services.AddSingleton (provider =>
        {
            var config = provider.GetRequiredService<IOptions<DepartmentConfigurations>>().Value;
            return new InsuranceApprovalRule(config.InsuranceApprovalDepartments);
        });

        services.AddSingleton(provider =>
        {
            var config = provider.GetRequiredService<IOptions<DepartmentConfigurations>>().Value;
            return new FinancialApprovalRule(config.FinancialApprovalDepartments);
        });

        services.AddSingleton<IValidationRule, AssignedToGpRule>(); 

        return services;
    }
}

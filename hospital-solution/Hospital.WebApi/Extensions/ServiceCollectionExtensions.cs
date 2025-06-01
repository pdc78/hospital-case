using Hospital.Application.DepartmentsConfiguration;
using Hospital.Application.Rules;
using Microsoft.Extensions.Options;

namespace Hospital.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddValidationRules(this IServiceCollection services)
    {
        services.AddSingleton(provider =>
        {
            var departmentsConfiguration = provider.GetRequiredService<IOptions<DepartmentsConfiguration>>().Value;
            return new ReferralRule(departmentsConfiguration.ReferralDepartments);
        });

        services.AddSingleton(provider =>
        {
            var departmentsConfiguration = provider.GetRequiredService<IOptions<DepartmentsConfiguration>>().Value;
            return new InsuranceApprovalRule(departmentsConfiguration.InsuranceApprovalDepartments);
        });

        services.AddSingleton(provider =>
        {
            var departmentsConfiguration = provider.GetRequiredService<IOptions<DepartmentsConfiguration>>().Value;
            return new FinancialApprovalRule(departmentsConfiguration.FinancialApprovalDepartments);
        });

        services.AddSingleton<AssignedToGpRule>();

        return services;
    }
}

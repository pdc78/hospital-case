using Hospital.Application.Configurations;
using Hospital.Application.Rules;
using Microsoft.Extensions.Options;

namespace Hospital.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddValidationRules(this IServiceCollection services)
    {
        services.AddSingleton(provider =>
        {
            var config = provider.GetRequiredService<IOptions<DepartmentsConfiguration>>().Value;
            return new ReferralRule(config.ReferralDepartments);
        });

        services.AddSingleton(provider =>
        {
            var config = provider.GetRequiredService<IOptions<DepartmentsConfiguration>>().Value;
            return new InsuranceApprovalRule(config.InsuranceApprovalDepartments);
        });

        services.AddSingleton(provider =>
        {
            var config = provider.GetRequiredService<IOptions<DepartmentsConfiguration>>().Value;
            return new FinancialApprovalRule(config.FinancialApprovalDepartments);
        });

        services.AddSingleton<AssignedToGpRule>();

        return services;
    }
}

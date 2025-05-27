namespace Hospital.WebApi.Configurations;
public class DepartmentConfigurations
{
    public List<string> FinancialApprovalDepartments { get; set; } = new();
    public List<string> InsuranceApprovalDepartments { get; set; } = new();
    public List<string> ReferralDepartments { get; set; } = new();
}

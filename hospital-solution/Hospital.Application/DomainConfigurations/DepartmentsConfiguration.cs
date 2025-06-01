namespace Hospital.Application.DepartmentsConfiguration;
public class DepartmentsConfiguration
{
    public Dictionary<string, List<string>> Rules { get; set; } = new();
    public List<string> ReferralDepartments { get; set; } = new();
    public List<string> InsuranceApprovalDepartments { get; set; } = new();
    public List<string> FinancialApprovalDepartments { get; set; } = new();
}
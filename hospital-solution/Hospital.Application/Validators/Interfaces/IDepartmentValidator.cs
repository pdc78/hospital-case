namespace Hospital.Application.Validators.Interfaces;

public interface IDepartmentValidator
{
    Task<(bool IsValid, string? ErrorMessage)> ValidateAsync(string cpr, string doctorName);
}

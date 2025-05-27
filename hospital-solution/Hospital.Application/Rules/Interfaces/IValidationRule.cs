namespace Hospital.Application.Rules.Interfaces;

public interface IValidationRule
{
    Task<(bool IsValid, string? ErrorMessage)> ValidateAsync(string cpr, string department);
}
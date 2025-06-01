namespace Hospital.Application.Services.Interfaces;
public interface INationalRegistryService
{
    /// <summary>
    /// Validates a CPR number against the national registry.
    /// </summary>
    /// <param name="cpr">The CPR number to validate.</param>
    /// <returns>True if the CPR is valid, otherwise false.</returns>
    Task<bool> ValidateCpr(string cpr);
}
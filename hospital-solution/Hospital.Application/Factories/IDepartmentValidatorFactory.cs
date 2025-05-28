using Hospital.Application.Validators.Interfaces;

namespace Hospital.Application.Factories;
public interface IDepartmentValidatorFactory
{
    IDepartmentValidator? GetValidator(string department);
}

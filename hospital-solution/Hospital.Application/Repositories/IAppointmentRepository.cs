using Hospital.Application.Entities;

namespace Hospital.Application.Repositories;

public interface IAppointmentRepository
{
    Task AddAsync(Appointment appointment);
    Task<Appointment?> GetByIdAsync(int id);
    Task<IEnumerable<Appointment>> GetAllAsync();
    Task UpdateAsync(Appointment appointment);
    Task DeleteAsync(int id);
}
using Hospital.Application.Entities;

namespace Hospital.Application.Repositories;

public class AppointmentRepository(AppointmentDbContext dbContext)
{
    public async Task AddAsync(Appointment appointment)
    {
        dbContext.Appointments.Add(appointment);
        await dbContext.SaveChangesAsync();
    }
}
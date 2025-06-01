using Hospital.Application.Data;
using Hospital.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Application.Repositories;

public class AppointmentRepository(AppointmentDbContext dbContext) : IAppointmentRepository
{
    public async Task AddAsync(Appointment appointment)
    {
        dbContext.Appointments.Add(appointment);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var appointment = await dbContext.Appointments.FindAsync(id);
        if (appointment is not null)
        {
            dbContext.Appointments.Remove(appointment);
            await dbContext.SaveChangesAsync();
        }
    }

    public Task<IEnumerable<Appointment>> GetAllAsync()
    {
        return dbContext.Appointments
            .AsNoTracking()
            .ToListAsync()
            .ContinueWith(task => task.Result.AsEnumerable());  
    }

    public Task<Appointment?> GetByIdAsync(int id)
    {
        return dbContext.Appointments
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public Task UpdateAsync(Appointment appointment)
    {
        dbContext.Appointments.Update(appointment);
        return dbContext.SaveChangesAsync();
    }
}
using Hospital.Application;
using Hospital.Application.Factories;
using Hospital.Application.Rules.Configurations;
using Hospital.WebApi.Configurations;
using Hospital.WebApi.Extensions;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSwaggerGen();

        builder.Services.Configure<DepartmentRulesConfiguration>(
            builder.Configuration.GetSection("DepartmentRules"));

        // Bind configuration section to a strongly-typed class
        builder.Services.Configure<DepartmentConfigurations>(
            builder.Configuration.GetSection("DepartmentConfigurations"));

        // Add services to the container.
        builder.Services.AddDbContext<AppointmentDbContext>(options =>
            options.UseInMemoryDatabase("HospitalDb"));


        // Register validation rules via extension method
        builder.Services.AddValidationRules();
        builder.Services.AddScoped<IDepartmentValidatorFactory, DepartmentValidatorFactory>();

        builder.Services.AddScoped<AppointmentRepository>();
        builder.Services.AddScoped<AppointmentService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Configure the HTTP request pipeline.
        app.UseHttpsRedirection();

        app.MapPost("/appointments", async (AppointmentRequest request, AppointmentService appointmentService) =>
        {
            var result = await appointmentService.ScheduleAppointment(
                request.Cpr, request.PatientName, request.AppointmentDate,
                request.Department, request.DoctorName);

            if (result)
                return Results.Ok("Appointment scheduled successfully.");
            else
                return Results.BadRequest("Failed to schedule the appointment.");
        });

        app.Run();
    }
}

// Updated AppointmentRequest model
public record AppointmentRequest(string Cpr, string PatientName, DateTime AppointmentDate, string Department, string DoctorName);
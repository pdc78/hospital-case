using Hospital.Application;
using Hospital.Application.Configurations;
using Hospital.Application.Factories;
using Hospital.Application.Repositories;
using Hospital.Application.Services;
using Hospital.Application.Services.Interfaces;
using Hospital.WebApi.Extensions;
using Microsoft.EntityFrameworkCore;
using Hospital.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddEndpointsApiExplorer(); // 👈 Required for minimal APIs
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Hospital API",
                Version = "v1"
            });
        });

        // Add Configurations for department in the App Settings
        builder.Services.Configure<DepartmentsConfiguration>(
            builder.Configuration.GetSection("Departments"));

        // Add services to the container.
        builder.Services.AddDbContext<AppointmentDbContext>(options =>
            options.UseInMemoryDatabase("HospitalDb"));


        // Register validation rules via extension method
        builder.Services.AddValidationRules();
        builder.Services.AddScoped<IDepartmentValidatorFactory, DepartmentValidatorFactory>();

        builder.Services.AddScoped<IAppointmentService, AppointmentService>();
        builder.Services.AddScoped<AppointmentRepository>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Configure the HTTP request pipeline.
        app.UseHttpsRedirection();

        app.MapPost("/appointments", async ([FromBody] AppointmentRequest request, [FromServices] IAppointmentService appointmentService) =>
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
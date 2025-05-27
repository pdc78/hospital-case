using Hospital.Application;
using Hospital.Application.Rules;
using Hospital.WebApi.Configurations;
using Hospital.WebApi.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Bind configuration section to a strongly-typed class
        builder.Services.Configure<DepartmentConfigurations>(
            builder.Configuration.GetSection("DepartmentConfigurations"));

        // Add services to the container.
        builder.Services.AddDbContext<AppointmentDbContext>(options =>
            options.UseInMemoryDatabase("HospitalDb"));
        builder.Services.AddScoped<AppointmentRepository>();
        builder.Services.AddScoped<AppointmentService>();


        // Register validation rules via extension method
        builder.Services.AddValidationRules();


        //// Register rules
        //builder.Services.AddSingleton(provider =>
        //{
        //    //var departments = new List<string> { "Surgey", "Radiology", "Physiotherapy" };
        //    var config = provider.GetRequiredService<IOptions<DepartmentConfigurations>>().Value;
        //    return new ReferralRule(config.ReferralDepartments);
        //});

        //builder.Services.AddSingleton(provider =>
        //{
        //    //var departments = new List<string> { "Physiotherapy" };
        //    var config = provider.GetRequiredService<IOptions<DepartmentConfigurations>>().Value;
        //    return new InsuranceApprovalRule(config.FinancialApprovalDepartments);
        //});

        //builder.Services.AddSingleton(provider =>
        //{
        //    //var departments = new List<string> { "Radiology" };
        //    var config = provider.GetRequiredService<IOptions<DepartmentConfigurations>>().Value;
        //    return new FinancialApprovalRule(config.FinancialApprovalDepartments);
        //});

        builder.Services.AddSingleton<AssignedToGpRule>();

        // Register the factory
        builder.Services.AddScoped<DepartmentValidatorFactory>();

        var app = builder.Build();

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
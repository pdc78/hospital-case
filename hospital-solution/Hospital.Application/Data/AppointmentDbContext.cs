﻿using Hospital.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Application.Data;

public class AppointmentDbContext : DbContext
{
    public AppointmentDbContext(DbContextOptions<AppointmentDbContext> options) : base(options) { }

    public DbSet<Appointment> Appointments { get; set; }
}
﻿using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StaffingSolution.Models;

namespace StaffingSolution.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<AdminSchedule> AdminSchedules { get; set; }
        public DbSet<Booking> Bookings { get; set; } 



    }
}

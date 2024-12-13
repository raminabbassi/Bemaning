using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StaffingSolution.Models;

namespace StaffingSolution.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<JobApplication> JobApplications { get; set; }


    }
}

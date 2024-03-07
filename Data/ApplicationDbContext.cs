using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Organization_Assessment_Project_NEW.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Organization_Assessment_Project_NEW.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveRequest> leaveRequests { get; set; }

        public DbSet<CreateLeaveRequest> createLeaveRequests { get; set; }

        public DbSet<Timesheet> timesheets { get; set; }
    }
}

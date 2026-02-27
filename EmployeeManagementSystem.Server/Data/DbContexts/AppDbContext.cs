using EmployeeManagementSystem.Core;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Server.Data.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    // Add Tables
    public DbSet<Employee> Employees { get; set; }
}
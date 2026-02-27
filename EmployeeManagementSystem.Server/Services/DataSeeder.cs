using EmployeeManagementSystem.Core;
using EmployeeManagementSystem.Server.Data.DbContexts;
using EmployeeManagementSystem.Server.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Server.Services;

public class DataSeeder(AppDbContext dbContext) : IDataSeeder
{
    public async Task SeedDataAsync()
    {
        if (await dbContext.Employees.AnyAsync())
            return;

        var employees = new List<Employee>
        {
            new()
            {
                Name = "أحمد الفارسي",
                Email = "ahmad.alfarsi@example.com",
                DateOfHiring = new DateTime(2020, 3, 15),
                Department = "Engineering"
            },
            new()
            {
                Name = "فاطمة الزهراء",
                Email = "fatima.alzahraa@example.com",
                DateOfHiring = new DateTime(2019, 7, 22),
                Department = "Human Resources"
            },
            new()
            {
                Name = "عمر المختار",
                Email = "omar.almukhtar@example.com",
                DateOfHiring = new DateTime(2021, 1, 10),
                Department = "Marketing"
            },
            new()
            {
                Name = "خالد بن سعيد",
                Email = "khalid.binsaeed@example.com",
                DateOfHiring = new DateTime(2022, 5, 3),
                Department = "Finance"
            },
            new()
            {
                Name = "ليلى العبدالله",
                Email = "layla.alabdullah@example.com",
                DateOfHiring = new DateTime(2023, 9, 18),
                Department = "IT"
            }
        };

        await dbContext.Employees.AddRangeAsync(employees);
        await dbContext.SaveChangesAsync();
    }
}
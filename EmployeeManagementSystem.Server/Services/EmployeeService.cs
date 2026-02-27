using EmployeeManagementSystem.Core;
using EmployeeManagementSystem.Server.Data.DbContexts;
using EmployeeManagementSystem.Server.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Server.Services;

public class EmployeeService(AppDbContext dbContext) : IEmployeeService
{
    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        return await dbContext.Employees.ToListAsync();
    }

    public async Task<IEnumerable<Employee>> SearchEmployeesAsync(string query)
    {
        var lowerQuery = query.ToLower();

        return await dbContext.Employees
            .Where(e => e.Id.ToString().Contains(lowerQuery) ||
                        e.Name.ToLower().Contains(lowerQuery) ||
                        e.Email.ToLower().Contains(lowerQuery) ||
                        e.DateOfHiring.ToString().Contains(lowerQuery) ||
                        e.Department.ToLower().Contains(lowerQuery))
            .ToListAsync();
    }

    public async Task<Employee?> GetEmployeeByIdAsync(int id)
    {
        return await dbContext.Employees.FindAsync(id);
    }

    public async Task<Employee> CreateEmployeeAsync(Employee employee)
    {
        dbContext.Employees.Add(employee);
        await dbContext.SaveChangesAsync();
        return employee;
    }

    public async Task<bool> UpdateEmployeeAsync(int id, Employee employee)
    {
        if (id != employee.Id)
            return false;

        dbContext.Entry(employee).State = EntityState.Modified;

        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await dbContext.Employees.AnyAsync(e => e.Id == id))
                return false;

            throw;
        }

        return true;
    }

    public async Task<bool> DeleteEmployeeAsync(int id)
    {
        var employee = await dbContext.Employees.FindAsync(id);
        if (employee == null)
            return false;

        dbContext.Employees.Remove(employee);
        await dbContext.SaveChangesAsync();
        return true;
    }
}


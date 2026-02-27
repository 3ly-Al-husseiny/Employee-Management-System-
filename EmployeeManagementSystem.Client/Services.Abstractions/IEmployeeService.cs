using EmployeeManagementSystem.Core;

namespace EmployeeManagementSystem.Client.Services.Abstractions;

public interface IEmployeeService
{
     Task<List<Employee>> GetAllEmployeesAsync();
     Task<List<Employee>> SearchEmployeesAsync(string query);
     Task<Employee?> GetEmployeeByIdAsync(int id);
     Task<Employee> CreateEmployeeAsync(Employee employee);
     Task<bool> UpdateEmployeeAsync(int id, Employee employee);
     Task<bool> DeleteEmployeeAsync(int id);
}
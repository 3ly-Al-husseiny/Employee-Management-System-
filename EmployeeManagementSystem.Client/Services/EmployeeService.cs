using System.Net.Http.Json;
using EmployeeManagementSystem.Client.Services.Abstractions;
using EmployeeManagementSystem.Core;

namespace EmployeeManagementSystem.Client.Services;

public class EmployeeService(HttpClient _httpClient) : IEmployeeService
{
    public async Task<List<Employee>> GetAllEmployeesAsync() =>
        await _httpClient.GetFromJsonAsync<List<Employee>>("api/employees") ?? new List<Employee>();


    public async Task<List<Employee>> SearchEmployeesAsync(string query) =>
        await _httpClient.GetFromJsonAsync<List<Employee>>($"api/employees/search?query={Uri.EscapeDataString(query)}") ?? new List<Employee>();


    public async Task<Employee?> GetEmployeeByIdAsync(int id) =>
        await _httpClient.GetFromJsonAsync<Employee>($"api/employees/{id}");


    public async Task<Employee> CreateEmployeeAsync(Employee employee)
    {
        await _httpClient.PostAsJsonAsync("api/employees", employee);
        return employee;
    }

    public async Task<bool> UpdateEmployeeAsync(int id, Employee employee)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/employees/{id}", employee);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteEmployeeAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/employees/{id}");
        return response.IsSuccessStatusCode;
    }
}
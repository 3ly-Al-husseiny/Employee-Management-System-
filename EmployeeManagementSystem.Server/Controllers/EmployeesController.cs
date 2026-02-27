using EmployeeManagementSystem.Core;
using EmployeeManagementSystem.Server.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController(IEmployeeService employeeService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAllEmployeesAsync()
    {
        var employees = await employeeService.GetAllEmployeesAsync();
        return Ok(employees);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchEmployeesAsync([FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest("Search query cannot be empty.");
        }

        var employees = await employeeService.SearchEmployeesAsync(query);
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployeeAsync(int id)
    {
        var employee = await employeeService.GetEmployeeByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployeeAsync(Employee employee)
    {
        var createdEmployee = await employeeService.CreateEmployeeAsync(employee);
        return CreatedAtAction(nameof(GetEmployeeAsync), new { id = createdEmployee.Id }, createdEmployee);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateEmployeeAsync(int id, Employee employee)
    {
        var updated = await employeeService.UpdateEmployeeAsync(id, employee);
        if (!updated)
        {
            return BadRequest();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployeeAsync(int id)
    {
        var deleted = await employeeService.DeleteEmployeeAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }

}
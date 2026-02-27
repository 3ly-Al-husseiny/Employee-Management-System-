using EmployeeManagementSystem.Core;
using EmployeeManagementSystem.Server.Data.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController(AppDbContext _dbContext) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAllEmployeesAsync()
    {
        var employees = await _dbContext.Employees.ToListAsync();
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployeeAsync(int id)
    {
        var employee = await _dbContext.Employees.FindAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployeeAsync(Employee employee)
    {
        _dbContext.Employees.Add(employee);
        await _dbContext.SaveChangesAsync();
        return CreatedAtAction(nameof(GetEmployeeAsync), new { id = employee.Id }, employee);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateEmployeeAsync(int id, Employee employee)
    {
        if (id != employee.Id)
        {
            return BadRequest();
        }

        _dbContext.Entry(employee).State = EntityState.Modified;

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            if (!EmployeeExists(id))
            {
                return NotFound();
            }
            else
            {
                throw ex;
            }
        }

        return NoContent();

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployeeAsync(int id)
    {
        var employee = await _dbContext.Employees.FindAsync(id);
        if (employee == null) return NotFound();
        
        _dbContext.Employees.Remove(employee);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

    private bool EmployeeExists(int id)
    {
        return _dbContext.Employees.Any(e => e.Id == id);
    }

}
namespace EmployeeManagementSystem.Core;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DateOfHiring { get; set; }
    public string Department { get; set; } = string.Empty;
}
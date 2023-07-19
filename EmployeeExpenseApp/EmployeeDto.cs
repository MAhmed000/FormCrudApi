using System;

public class EmployeeDto
{
    public Guid EmployeeId { get; set; }
    public string EmployeeName { get; set; } = null!;
    public string Contact { get; set; } = null!;
    public string Address { get; set; } = null!;
}

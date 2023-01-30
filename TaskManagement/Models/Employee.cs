
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models;

public partial class Employee
{
    [Key]
    public int EmployeeId { get; set; }

    public string Mid { get; set; } = null!;

    public string EmployeeName { get; set; } = null!;

    public virtual ICollection<TaskModel> Tasks { get; } = new List<TaskModel>();
}

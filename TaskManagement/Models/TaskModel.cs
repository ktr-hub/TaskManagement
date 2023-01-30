using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models;

public partial class TaskModel
{
    public TaskModel(string TaskDescription, DateTime StartDate, DateTime DueDate,
                                            int ProjectId,
                                            int EmployeeId)
    {
        this.TaskDescription = TaskDescription;
        this.StartDate= StartDate;
        this.DueDate = DueDate;
        this.ProjectId = (int)ProjectId;
        this.EmployeeId = (int)EmployeeId;
    }

    [Key]
    public int TaskId { get; set; }

    public string TaskDescription { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime DueDate { get; set; }

    public int ProjectId { get; set; }

    public int EmployeeId { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}

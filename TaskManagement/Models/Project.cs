using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models;

public partial class Project
{
    [Key]
    public int ProjectId { get; set; }

    public string ProjectName { get; set; } = null!;

    public virtual ICollection<TaskModel> Tasks { get; } = new List<TaskModel>();
}

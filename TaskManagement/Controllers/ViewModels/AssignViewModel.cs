using Microsoft.EntityFrameworkCore;
using TaskManagement.Models;

namespace TaskManagement.Controllers.ViewModels
{
    public class AssignViewModel
    {
        public bool IsMessageDisplayed { get; set; }
        public string Message { get; set; }
        public List<Employee>? Employees { get; set; }
        public List<Project>? Projects { get; set; }
        public List<TaskModel>? Tasks { get; set; }
        public string? TaskDescription { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int? SelectedEmployeeId { get; set; }
        public int? SelectedProjectId { get; set; }

        public AssignViewModel()
        {
            Projects = new List<Project>();
            Employees = new List<Employee>();
            Tasks = new List<TaskModel>();
        }

        public AssignViewModel(List<Project>? Projects,List<Employee>? Employees) : base()
        {
            this.Projects = Projects;
            this.Employees = Employees;
        }
        public AssignViewModel(List<Project>? Projects, List<Employee>? Employees, List<TaskModel>? tasks ): base()
        {
            this.Projects = Projects;
            this.Employees = Employees;
            this.Tasks = tasks;
        }
    }
}

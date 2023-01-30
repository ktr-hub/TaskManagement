using Microsoft.EntityFrameworkCore;
using TaskManagement.Models;

namespace TaskManagement.Controllers.ViewModels
{
    public class ViewTasksViewModel
    {
        private ViewTasksViewModel _instance;
        public bool IsMessageDisplayed { get; set; }
        public string Message { get; set; }

        public int SelectedProjectId;
        public List<Employee>? Employees { get; set; }
        public List<Project>? Projects { get; set; }
        public List<TaskModel>? Tasks { get; set; }
        public ViewTasksViewModel()
        {
            if (_instance == null)
            {
                Projects = new List<Project>();
                Employees = new List<Employee>();
                Tasks = new List<TaskModel>();
                _instance = this;
            }
        }

        public ViewTasksViewModel(List<Project>? Projects, List<Employee>? Employees, List<TaskModel>? tasks ) : base()
        {
            this.Projects = Projects;
            this.Employees = Employees;
            this.Tasks = tasks;
        }
    }
}

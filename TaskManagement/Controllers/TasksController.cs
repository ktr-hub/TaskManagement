using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using TaskManagement.Controllers.ViewModels;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    public class TasksController : Controller
    {
        private readonly TaskManagementDbContext _context;
        public ViewTasksViewModel viewTasksViewModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public TasksController()
        {
            _context = TaskManagementDbContext.GetInstance();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: TasksController/Assign

        [Route("tasks/assign/")]
        public IActionResult Assign(AssignViewModel viewModel)
        {
            if(viewModel == null)
                viewModel = new AssignViewModel();

            if (_context != null && _context.Projects != null && _context.Employees != null)
            {
                viewModel = new AssignViewModel(  _context.Projects.ToList(), _context.Employees.ToList());
                return View(viewModel);
            }
            return View(viewModel);
        }
        public JsonResult GetSelectedTasks(int id)
        {
            var tasks = _context.Tasks.Where(t => t.ProjectId == id).OrderBy(tt => tt.TaskId).ThenBy(ttt => ttt.EmployeeId).ToList();
            return Json(tasks);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult ViewTasks(int id)
        {
            //if (id != 0)
            //{
            //    GetSelectedTasks(id);
            //    return View(viewTasksViewModel);
            //}
            //viewTasksViewModel = new ViewTasksViewModel();
            //viewTasksViewModel.Projects = _context.Projects.ToList();
            //viewTasksViewModel.Employees = _context.Employees.ToList();
            //viewTasksViewModel.Tasks = _context.Tasks.OrderBy(t=>t.ProjectId).ThenBy(tt => tt.TaskId).ThenBy(ttt => ttt.EmployeeId).ToList();
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetProjectById(int id)
        {
            if(id == 0)
            {
                return RedirectToAction("GetProjectsData");
            }
            // Call the stored procedure and fetch the data
            var project = await _context.Projects.Where(project => project.ProjectId == id).ToListAsync();

            // Return the data as JSON
            return Json(project);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public async Task<IActionResult> GenerateDifferentTasksByProjectId(int id)
        {
            // Call the stored procedure and fetch the data
            var tasks = await _context.Tasks.FromSqlInterpolated($"EXECUTE dbo.GenerateDifferentTasksByProjectId @Id = {id}").ToListAsync();

            // Return the data as JSON
            return Json(tasks);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetProjectsData()
        {
            // Call the stored procedure and fetch the data
            var projects = await _context.Projects.FromSqlInterpolated($"EXECUTE dbo.GetProjectsData").ToListAsync();

            // Return the data as JSON
            return Json(projects);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetTasksData()
        {
            // Call the stored procedure and fetch the data
            var tasks = await _context.Tasks.FromSqlInterpolated($"EXECUTE dbo.GetTasksData").ToListAsync();

            // Return the data as JSON
            return Json(tasks);
        }


        [HttpGet]
        public async Task<JsonResult> DropdownData()
        {

            var tasks = await _context.Tasks.ToListAsync();
            string jsonString = JsonSerializer.Serialize(GetTasks(0));
            await Response.WriteAsync(jsonString);
            return Json(tasks);
        }

        public List<TaskModel> GetTasks(int id)
        {
            var parameters = new SqlParameter[]
            {
            new SqlParameter("@Task_Id", id)
            };
            List<TaskModel> result;
            if (id == 0)
            {
                //get all tasks
                result = (List<TaskModel>)_context.ExecuteStoredProcedure<List<TaskModel>>("GetTasks", parameters);

            }
            else
            {
                result = (List<TaskModel>)_context.ExecuteStoredProcedure<List<TaskModel>>("GetTasks", parameters);

            }
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskModel"></param>
        /// <returns></returns>
        // POST: TasksController/Create
        [HttpPost]
        public ActionResult Create(AssignViewModel viewModel)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                    if (viewModel.SelectedProjectId != null &&
                                                viewModel.SelectedEmployeeId != null &&
                                                viewModel.TaskDescription != null &&
                                                viewModel.StartDate != null &&
                                                viewModel.DueDate != null)
                    {
                        Employee? employee = _context.Employees.FirstOrDefault(x => x.EmployeeId == (int)viewModel.SelectedEmployeeId);
                        TaskManagement.Models.Project? project = _context.Projects.FirstOrDefault(x => x.ProjectId == (int)viewModel.SelectedProjectId);

                        if (employee != null && project != null)
                        {
                            var task = new TaskModel(viewModel.TaskDescription,
                                                    (DateTime)viewModel.StartDate,
                                                    (DateTime)viewModel.DueDate,
                                                    (int)viewModel.SelectedProjectId,
                                                    (int)viewModel.SelectedEmployeeId
                                                );
                            _context.Tasks.Add(task);
                            _context.SaveChanges();
                            Console.WriteLine("Saved Task");
                            return RedirectToAction("Index", "index");
                        }
                    }
                    else
                    {
                        viewModel.IsMessageDisplayed = false;
                        viewModel.Message = "Enter all the required details";
                        Console.WriteLine("Enter all the required details");
                        return RedirectToAction("Assign", viewModel);
                    }
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed");
                System.Diagnostics.Trace.WriteLine(ex.ToString());
                return View("Error");
            }
            viewModel.IsMessageDisplayed = false;
            viewModel.Message = "Enter all the required details";
            Console.WriteLine("Enter all the required details");
            return RedirectToAction("Assign");
        }

        public ActionResult DisplayCreate()
        {
            //Add successful message
            HomeViewModel viewModel = new HomeViewModel();
            viewModel.IsDataInserted = true;
            return RedirectToAction("Tasks","Create");
        }

        // GET: TasksController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TasksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TasksController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TasksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

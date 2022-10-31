using EmployeeData.DAL;
using EmployeeData.Models;
using EmployeeData.Models.DbEntites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.ExceptionServices;

namespace EmployeeData.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext context;

        public EmployeeController(EmployeeDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var employees = context.Employees.ToList();
            List<EmployeeViewModel> employeeList = new List<EmployeeViewModel>();
            if (employees !=null)
            {
                
                foreach(var employee in employees)
                {
                    var EmployeeViewModel = new EmployeeViewModel()
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,   
                        DateofBirth = employee.DateofBirth, 
                        Email = employee.Email,
                        Salary =    employee.Salary,

                    };
                    employeeList.Add(EmployeeViewModel);
                }
                return View(employeeList);
            }

            return View(employeeList);
        }
        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Create(EmployeeViewModel employeeData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employee = new Employee()
                    {
                        FirstName = employeeData.FirstName,
                        LastName = employeeData.LastName,
                        DateofBirth = employeeData.DateofBirth,
                        Email = employeeData.Email,
                        Salary = employeeData.Salary
                    };
                    context.Employees.Add(employee);
                    context.SaveChanges();
                    TempData["successMessage"] = "Employee created successfully!";
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["errorMessage"] = "Model data is not valid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            
        }
        [HttpGet]

         public IActionResult Edit(int Id)
        {
            try
            {
                var employee = context.Employees.SingleOrDefault(x => x.Id == Id);
                if (employee != null)
                {
                    var employeeView = new EmployeeViewModel()
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        DateofBirth = employee.DateofBirth,
                        Email = employee.Email,
                        Salary = employee.Salary
                    };
                    return View(employeeView);
                }
                else
                {
                    TempData["errorMessage"] = $"Employee Details are not Present with the Id:{Id}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
        [HttpPost]

        public IActionResult Edit(EmployeeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employee = new Employee()
                    {
                        Id = model.Id,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        DateofBirth = model.DateofBirth,
                        Email = model.Email,
                        Salary = model.Salary
                    };
                    context.Employees.Update(employee);
                    context.SaveChanges();
                    TempData["successMessage"] = "Employee created successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Employee Data is not Valid!";
                    return View();
                }
            }
            catch (Exception ex) 
            {
                TempData["errorMessage"] = ex.Message;
                return View();

            }

         }


        [HttpGet]

        public IActionResult Delete(int Id)
        {
            try
            {
                var employee = context.Employees.SingleOrDefault(x => x.Id == Id);
                if (employee != null)
                {
                    var employeeView = new EmployeeViewModel()
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        DateofBirth = employee.DateofBirth,
                        Email = employee.Email,
                        Salary = employee.Salary
                    };
                    return View(employeeView);
                }
                else
                {
                    TempData["errorMessage"] = $"Employee Details are not Present with the Id:{Id}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Delete(EmployeeViewModel model)
        {
            try
            {
                var employee = context.Employees.SingleOrDefault(x => x.Id == model.Id);
                if (employee != null)
                {
                    context.Employees.Remove(employee);
                    context.SaveChanges();
                    TempData["successMessage"] = "Employee Deleted successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = $"Employee Data is not Valid! Id:{model.Id}";
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
    }
}

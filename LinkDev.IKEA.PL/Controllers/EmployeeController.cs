﻿using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.DAL.Entities.Employee;
using LinkDev.IKEA.PL.ViewModels.Departments;
using LinkDev.IKEA.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    // Inheritance: DepartmentController is a Controller
    // Composition: Employee has a IDepartmentService 

    public class EmployeeController : Controller
    {
        #region Services
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _environment;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger, IWebHostEnvironment environment)
        {
            _employeeService = employeeService;
            _logger = logger;
            _environment = environment;
        }
        #endregion

        #region Index
        [HttpGet] // GET: /Employee/Index
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployees();

            return View(employees);
        }
        #endregion

        #region Create
        [HttpGet] // GET: /Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] // Post: /Employee/Create
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatedEmployeeDto Employee)
        {
            if (!ModelState.IsValid) // Server Side Validation 
                return View(Employee);

            var Message = string.Empty;
            try
            {
                var Result = _employeeService.CreateEmployee(Employee);

                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    Message = "Employee is not Created";
                    ModelState.AddModelError(string.Empty, Message);
                    return View(Employee);
                }

            }
            catch (Exception ex)
            {
                // 1. Log Exception
                _logger.LogError(ex, ex.Message);

                // 2. Set Message

                Message = _environment.IsDevelopment() ? ex.Message : "An Error has occured during Creating this Employee :(";


            }
            ModelState.AddModelError(string.Empty, Message);
            return View(Employee);

        }
        #endregion

        #region Details
        [HttpGet] //  Get: /Employee/Details
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee = _employeeService.GetEmployeeById(id.Value);

            if (employee is null)
                return NotFound();

            return View(employee);

        }
        #endregion

        #region Edit
        [HttpGet] // GET: /Employee/Edit/id?
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest(); // 400

            var Employee = _employeeService.GetEmployeeById(id.Value);

            if (Employee is null)
                return NotFound();  // 404

            return View(new UpdatedEmployeetDto()
            {
                Name = Employee.Name,
                Address = Employee.Address,
                Email = Employee.Email,
                Age = Employee.Age,
                Salary = Employee.Salary,
                HiringDate = Employee.HiringDate,
                IsActive = Employee.IsActive,
                PhoneNumber = Employee.PhoneNumber,
                EmplyeeType = Employee.EmplyeeType,
                Gender = Employee.Gender,
            });
        }

        [HttpPost] // Post: 
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, UpdatedEmployeetDto Employee)
        {
            if (!ModelState.IsValid) // Server Side Validation
                return View(Employee);

            var Message = string.Empty;

            try
            {


                var Updated = _employeeService.UpdateEmployee(Employee) > 0;

                if (Updated)
                    return RedirectToAction(nameof(Index));

                Message = "An Error has occured during updating this Employee :(";
            }
            catch (Exception ex)
            {

                // 1. Log Exception
                _logger.LogError(ex, ex.Message);

                // 2.Set Message
                Message = _environment.IsDevelopment() ? ex.Message : "An Error has occured during updating this Employee :(";

            }
            ModelState.AddModelError(string.Empty, Message);
            return View(Employee);
        } 
        #endregion

        #region Delete
      
        [HttpPost] // Post: 
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var Message = string.Empty;

            try
            {
                var deleted = _employeeService.DeleteEmployee(id);

                if (deleted)
                    return RedirectToAction(nameof(Index));

                Message = "An Error has occured during Deleting this Employee :(";
            }
            catch (Exception ex)
            {

                // 1. Log Exception
                _logger.LogError(ex, ex.Message);

                // 2.Set Message
                Message = _environment.IsDevelopment() ? ex.Message : "An Error has occured during Deleting this Employee :(";
            }

            //ModelState.AddModelError(string.Empty , Message);
            return RedirectToAction(nameof(Index));

        }
        #endregion
    }
}

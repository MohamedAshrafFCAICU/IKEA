using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.DAL.Entities.Department;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    // Inheritance: DepartmentController is a Controller
    // Composition: Department has a IDepartmentService 

    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(IDepartmentService departmentService , ILogger<DepartmentController> logger , IWebHostEnvironment environment)
        {
            _departmentService = departmentService;
            _logger = logger;
            _environment = environment;
        }

        [HttpGet] // GET: /Department/Index
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();

            return View(departments);
        }

        [HttpGet] // GET: /Department/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] // Post: /Department/Create
        public IActionResult Create(CreatedDepartmentDto department)
        {
             if(!ModelState.IsValid)
                return View(department);

            var Message = string.Empty;
            try
            {
                var Result = _departmentService.CreateDepartment(department);

                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    Message = "Department is not Created";
                    ModelState.AddModelError(string.Empty, Message);
                    return View(department);
                }
                  
    }
            catch (Exception ex)
            {
                // 1. Log Exception
                _logger.LogError(ex, ex.Message);

                // 2.Set Message
                if (_environment.IsDevelopment())
                    Message = ex.Message;
                else
                    Message = "Department is not Created";

                return View("Error", Message);
            }
        }

        [HttpGet] //  Get: /Department/Details
        public IActionResult Details(int? id)
        {
            if(id is null)
                return BadRequest();

            var department = _departmentService.GetDepartmentById(id.Value);

            if(department is null)
                return NotFound();

            return View(department);    

        }
    }
}

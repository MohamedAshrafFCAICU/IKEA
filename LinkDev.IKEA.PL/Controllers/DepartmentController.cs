using AutoMapper;
using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.DAL.Entities.Department;
using LinkDev.IKEA.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    // Inheritance: DepartmentController is a Controller
    // Composition: Department has a IDepartmentService 

    public class DepartmentController : Controller
    {
        #region Services
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

       
        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger, IWebHostEnvironment environment , IMapper mapper)
        {
            _departmentService = departmentService;
            _logger = logger;
            _environment = environment;
            _mapper = mapper;
        }
        #endregion

        #region Index
        [HttpGet] // GET: /Department/Index
        public async Task<IActionResult> Index()
        {
            // View 's Disctonary => Passing Data From Controller [Action] To View (From View -> [Layout- Partial View])


            //1. ViewData =>  Disctonary: is a Dictionary Type Property (introduced in ASP.NET Framework 3.
            /// => It helps us To Transfer The Data from Controller[Action] => View

            ViewData["Message"] = "Hello View Data";


            //1. ViewBag =>  Disctonary: is a Dynamic Dictionary Type Property (introduced in ASP.NET Framework 4
            /// => It helps us To Transfer The Data from Controller[Action] => View

            ViewBag.Message = "Hello View Bag";


            ViewData["Message"] = "Hello View";

            var departments =await _departmentService.GetAllDepartmentsAsync();

            return View(departments);
        }
        #endregion

        #region Create
        [HttpGet] // GET: /Department/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] // Post: /Department/Create
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentEditViewModel departmentVM)
        {
            if (!ModelState.IsValid)
                return View(departmentVM);

            var Message = string.Empty;
            try
            {

                // Manual Mapping
                //var department = new CreatedDepartmentDto()
                //{
                //    Code = departmentVM.Code,
                //    Name = departmentVM.Name,
                //    CreationDate = departmentVM.CreationDate,
                //    Description = departmentVM.Description,
                //};

                var CreatedDepartment = _mapper.Map<DepartmentEditViewModel, CreatedDepartmentDto>(departmentVM);


                var Created = await _departmentService.CreateDepartmentAsync(CreatedDepartment) > 0;


                // 3. TempData : is a property of type of Dictonary(object)(ASP.Net Framwork 3.5)
                //  Used To Transfer Data between 2 Consuctive requests

                if (!Created)
                {
                    Message = "Department is not Created";

                    ModelState.AddModelError(string.Empty, Message);
                    return View(departmentVM);
                }

            }
            catch (Exception ex)
            {
                // 1. Log Exception
                _logger.LogError(ex, ex.Message);

                // 2. Set Message

                Message = _environment.IsDevelopment() ? ex.Message : "An Error has occured during Creating this department :(";

                TempData["Message"] = Message;
                return  RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region Details
        [HttpGet] //  Get: /Department/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);

            if (department is null)
                return NotFound();

            return View(department);

        }
        #endregion

        #region Edit
        [HttpGet] // GET: /Department/Edit/id?
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest(); // 400

            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);

            if (department is null)
                return NotFound();  // 404

            var departmntVM = _mapper.Map<DepartmentDetailsToReturnDto, DepartmentEditViewModel>(department);

            return View(departmntVM);
        }

        [HttpPost] // Post: 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, DepartmentEditViewModel department)
        {
            if (id is null)
                return BadRequest();

            if (!ModelState.IsValid) // Server Side Validation
                return View(department);

            var Message = string.Empty;

            try
            {
                //var departmentToUpdate = new UpdatedDepartmentDto()
                //{
                //    Id = id,
                //    Code = department.Code,
                //    Name = department.Name,
                //    Description = department.Description,
                //    CreationDate = department.CreationDate
                //};

                var UpdatedDepartment = _mapper.Map<DepartmentEditViewModel, UpdatedDepartmentDto>(department);

                var Updated = await _departmentService.UpdateDepartmentAsync(UpdatedDepartment) > 0;

                if (Updated)
                    return RedirectToAction(nameof(Index));

                Message = "An Error has occured during updating this department :(";
            }
            catch (Exception ex)
            {

                // 1. Log Exception
                _logger.LogError(ex, ex.Message);

                // 2.Set Message
                Message = _environment.IsDevelopment() ? ex.Message : "An Error has occured during updating this department :(";

            }
            ModelState.AddModelError(string.Empty, Message);
            return View(department);
        }
        #endregion

        #region Delete
        [HttpGet] // Get: /Department/Delete/id?
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);

            if (department is null)
                return NotFound();

            return View(department);
        }

        [HttpPost] // Post: 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var Message = string.Empty;

            try
            {
                var deleted = await _departmentService.DeleteDepartmentAsync(id);

                if (deleted)
                    return RedirectToAction(nameof(Index));

                Message = "An Error has occured during Deleting this department :(";
            }
            catch (Exception ex)
            {

                // 1. Log Exception
                _logger.LogError(ex, ex.Message);

                // 2.Set Message
                Message = _environment.IsDevelopment() ? ex.Message : "An Error has occured during Deleting this department :(";
            }

            //ModelState.AddModelError(string.Empty , Message);
            return RedirectToAction(nameof(Index));

        } 
        #endregion
    }
}

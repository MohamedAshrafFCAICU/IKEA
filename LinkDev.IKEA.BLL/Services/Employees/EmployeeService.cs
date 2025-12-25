using LinkDev.IKEA.BLL.Common.Services.Attachments;
using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.DAL.Entities.Employee;
using LinkDev.IKEA.DAL.Persistance.Repositories.Employees;
using LinkDev.IKEA.DAL.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentService _attachmentService;

        public EmployeeService(IUnitOfWork unitOfWork , IAttachmentService attachmentService)
        {
           _unitOfWork = unitOfWork;
           _attachmentService = attachmentService;
        }

        public async Task<int> CreateEmployeeAsync(CreatedEmployeeDto Employee)
        {
            var employee = new Employee()
            {
                Name = Employee.Name,
                Age = Employee.Age,
                Address = Employee.Address,
                IsActive = Employee.IsActive,
                Salary = Employee.Salary,   
                Email = Employee.Email, 
                PhoneNumber = Employee.PhoneNumber,
                HiringDate = Employee.HiringDate,
                Gender = Employee.Gender,


                EmplyeeType = Employee.EmplyeeType,
                DepartmentId = Employee.DepartmentId,

                CreatedBy  = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };

            if (Employee.Image is not null)
                employee.Image =await  _attachmentService.UploadAsync(Employee.Image, "Images");
            
             _unitOfWork.EmployeeRepository.Add(employee);
            return await _unitOfWork.CompleteAsync();
            
        }

        public async Task<int> UpdateEmployeeAsync(UpdatedEmployeetDto Employee)
        {
            var employee = new Employee()
            {
                Id = Employee.Id,   
                Name = Employee.Name,
                Age = Employee.Age,
                Address = Employee.Address,
                IsActive = Employee.IsActive,
                Salary = Employee.Salary,
                Email = Employee.Email,
                PhoneNumber = Employee.PhoneNumber,
                HiringDate = Employee.HiringDate,
                Gender = Employee.Gender,
                EmplyeeType = Employee.EmplyeeType,
                DepartmentId = Employee.DepartmentId,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };

          _unitOfWork.EmployeeRepository.Update(employee);

            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employeeRepo = _unitOfWork.EmployeeRepository;

            var employee = await employeeRepo.GetByIdAsync(id);
            if (employee is { })
                employeeRepo.Delete(employee);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<IEnumerable<EmployeeToReturnDto>> GetEmployeesAsync(string search)
        {
            

            var employees = await _unitOfWork.EmployeeRepository.GetAllAsIQueryable().Where(E => !E.IsDeleted  && (string.IsNullOrEmpty(search) || E.Name.ToLower().Contains(search.ToLower()))).Include(E => E.Department).Select(employee => new EmployeeToReturnDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                IsActive = employee.IsActive,
                Salary = employee.Salary,
                Email = employee.Email,
                Gender = employee.Gender.ToString(),
                EmplyeeType =employee.EmplyeeType.ToString(),
               Department = employee.Department!.Name,
            }).ToListAsync();

            return employees;
        }

        public async Task<EmployeeDetailsToReturnDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
            
            if(employee is { })
                return  new EmployeeDetailsToReturnDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    IsActive = employee.IsActive,
                    Salary = employee.Salary,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    HiringDate = employee.HiringDate,
                    Gender = employee.Gender,
                    Image = employee.Image, 

                    EmplyeeType =employee.EmplyeeType,
                    Department = employee.Department!.Name,

                    CreatedBy = employee.CreatedBy,
                    CreatedOn = employee.CreatedOn,
                    LastModifiedBy = employee.LastModifiedBy,
                    LastModifiedOn = employee.LastModifiedOn,
                    
                };

            return null;
        }


    }
}

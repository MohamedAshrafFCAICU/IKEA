using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.DAL.Entities.Employee;
using LinkDev.IKEA.DAL.Persistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
         
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public int CreateEmployee(CreatedEmployeeDto Employee)
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
                CreatedBy  = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };
            
           return _employeeRepository.Add(employee);
            
        }

        public int UpdateEmployee(UpdatedEmployeetDto Employee)
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
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };

            return _employeeRepository.Update(employee);
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is { })
                return _employeeRepository.Delete(employee) > 0;
            return false;
        }

        public IEnumerable<EmployeeToReturnDto> GetAllEmployees()
        {
            var employees = _employeeRepository.GetAll();

            return _employeeRepository.GetAllAsIQueryable().Select(employee => new EmployeeToReturnDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                IsActive = employee.IsActive,
                Salary = employee.Salary,
                Email = employee.Email,
                Gender = nameof(employee.Gender),
                EmplyeeType =nameof(employee.EmplyeeType),
               
            });
        }

        public EmployeeDetailsToReturnDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if(employee is { })
                return new EmployeeDetailsToReturnDto()
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
                    Gender = nameof(employee.Gender),
                    EmplyeeType = nameof(employee.EmplyeeType),
                };

            return null;
        }


    }
}

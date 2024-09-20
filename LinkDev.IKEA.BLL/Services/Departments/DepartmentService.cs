using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.DAL.Entities.Department;
using LinkDev.IKEA.DAL.Persistance.Repositories.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public int CreateDepartment(CreatedDepartmentDto department)
        {
            var _department = new Department()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
                CreatedBy = 1,
                CreatedOn =DateTime.UtcNow,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };

            return _departmentRepository.Add(_department);
        }
      
        public int UpdateDepartment(UpdatedDepartmentDto department)
        {
            var _department = new Department()
            {
                Id = department.Id, 
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
                CreatedBy = 1,
                CreatedOn = DateTime.UtcNow,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };

            return _departmentRepository.Update(_department);
        }

        public bool DeleteDepartment(int id)
        {
            var department  = _departmentRepository.GetById(id);   
            if(department is { })
               return _departmentRepository.Delete(department) > 0;

            return false; 
        }

        public IEnumerable<DepartmentToReturnDto> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAll();

            foreach (var department in departments) 
            {
                yield return new DepartmentToReturnDto()
                {
                    Code = department.Code,
                    Name = department.Name,
                    Description = department.Description,
                    CreationDate = department.CreationDate, 
                };
            }
        }

        public DepartmentDetailsToReturnDto? GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetById(id);
            
            if(department is { })
                 return new DepartmentDetailsToReturnDto()
            {
               Id = department!.Id,
               Code = department.Code,
               Name= department.Name,
               Description= department.Description,
               CreationDate = department.CreationDate,
               CreatedBy = department.CreatedBy,
               CreatedOn = department.CreatedOn,    
               LastModifiedBy = department.LastModifiedBy,
               LastModifiedOn= department.LastModifiedOn    
            };
            else
                return null;
        }

     
    }
}

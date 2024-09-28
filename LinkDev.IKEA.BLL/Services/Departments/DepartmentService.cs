using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.DAL.Entities.Department;
using LinkDev.IKEA.DAL.Persistance.Repositories.Departments;
using LinkDev.IKEA.DAL.Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
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

           _unitOfWork.DepartmentRepository.Add(_department);

            return _unitOfWork.Complete();
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

            _unitOfWork.DepartmentRepository.Update(_department);

            return _unitOfWork.Complete();
        }

        public bool DeleteDepartment(int id)
        {
            var departmentRepo = _unitOfWork.DepartmentRepository;

            var department  = departmentRepo.GetById(id);   
            if(department is { })
              departmentRepo.Delete(department);

            return _unitOfWork.Complete() > 0; 
        }

        public IEnumerable<DepartmentToReturnDto> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();

            foreach (var department in departments) 
            {
                yield return new DepartmentToReturnDto()
                {
                    Id = department.Id, 
                    Code = department.Code,
                    Name = department.Name,
                    CreationDate = department.CreationDate, 
                };
            }
        }

        public DepartmentDetailsToReturnDto? GetDepartmentById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            
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

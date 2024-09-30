using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.DAL.Entities.Department;
using LinkDev.IKEA.DAL.Persistance.Repositories.Departments;
using LinkDev.IKEA.DAL.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public async Task<int> CreateDepartmentAsync(CreatedDepartmentDto department)
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

            return await _unitOfWork.CompleteAsync();
        }
      
        public async Task<int> UpdateDepartmentAsync(UpdatedDepartmentDto department)
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

            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var departmentRepo = _unitOfWork.DepartmentRepository;

            var department  = await departmentRepo.GetByIdAsync(id);   
            if(department is { })
              departmentRepo.Delete(department);

            return await _unitOfWork.CompleteAsync() > 0; 
        }

        public async Task<IEnumerable<DepartmentToReturnDto>> GetAllDepartmentsAsync()
        {
            var departmentRepo =  _unitOfWork.DepartmentRepository;

            var departments = await departmentRepo.GetAllAsIQueryable()
                .Where(D => !D.IsDeleted).Select(department => new DepartmentToReturnDto()
                {
                    Id = department.Id,
                    Code = department.Code,
                    Name = department.Name,
                    CreationDate = department.CreationDate
                }).AsNoTracking().ToListAsync();

            return  departments;
        }

        public async Task<DepartmentDetailsToReturnDto?> GetDepartmentByIdAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);
            
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

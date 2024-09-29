using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.DAL.Entities.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Departments
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentToReturnDto>> GetAllDepartmentsAsync();

        Task<DepartmentDetailsToReturnDto?> GetDepartmentByIdAsync(int id);

        Task<int> CreateDepartmentAsync(CreatedDepartmentDto department);

        Task<int> UpdateDepartmentAsync(UpdatedDepartmentDto department);

        Task<bool> DeleteDepartmentAsync(int id);  
    }
}

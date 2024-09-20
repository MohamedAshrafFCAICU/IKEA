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
        IEnumerable<DepartmentToReturnDto> GetAllDepartments();

        DepartmentDetailsToReturnDto? GetDepartmentById(int id);

        int CreateDepartment(CreatedDepartmentDto department);

        int UpdateDepartment(UpdatedDepartmentDto department);

        bool DeleteDepartment(int id);  
    }
}

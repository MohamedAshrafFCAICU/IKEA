using LinkDev.IKEA.BLL.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Employees
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeToReturnDto>> GetEmployeesAsync(string search);

        Task<EmployeeDetailsToReturnDto?> GetEmployeeByIdAsync(int id);

        Task<int> CreateEmployeeAsync(CreatedEmployeeDto Employee);

        Task<int> UpdateEmployeeAsync(UpdatedEmployeetDto Employee);

        Task<bool> DeleteEmployeeAsync(int id);
       
    }
}

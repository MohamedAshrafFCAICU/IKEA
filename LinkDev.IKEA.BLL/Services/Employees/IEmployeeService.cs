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
        IEnumerable<EmployeeToReturnDto> GetEmployees(string search);

        EmployeeDetailsToReturnDto? GetEmployeeById(int id);

        int CreateEmployee(CreatedEmployeeDto Employee);

        int UpdateEmployee(UpdatedEmployeetDto Employee);

        bool DeleteEmployee(int id);
    }
}

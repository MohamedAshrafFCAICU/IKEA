using LinkDev.IKEA.DAL.Persistance.Repositories.Departments;
using LinkDev.IKEA.DAL.Persistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IDepartmentRepository DepartmentRepository { get; }

        public IEmployeeRepository EmployeeRepository { get; }

        int Complete();
            
    }
}

using LinkDev.IKEA.DAL.Entities.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.Repositories.Departments
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllAsync(bool WithAsNoTracking = true);

        IQueryable<Department> GetAllAsIQueryable();

        Task<Department?> GetByIdAsync(int id);

        void Add(Department entity);

        void Update(Department entity);

        void Delete(Department entity);
    }
}

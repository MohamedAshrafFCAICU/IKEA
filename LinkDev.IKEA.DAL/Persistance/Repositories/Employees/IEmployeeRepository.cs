using LinkDev.IKEA.DAL.Entities.Employee;

namespace LinkDev.IKEA.DAL.Persistance.Repositories.Employees
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync(bool WithAsNoTracking = true);

        IQueryable<Employee> GetAllAsIQueryable();

        Task<Employee?> GetByIdAsync(int id);

        void Add(Employee entity);

        void Update(Employee entity);

        void Delete(Employee entity);
    }
}

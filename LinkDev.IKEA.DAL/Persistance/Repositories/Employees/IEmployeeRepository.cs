using LinkDev.IKEA.DAL.Entities.Employee;

namespace LinkDev.IKEA.DAL.Persistance.Repositories.Employees
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll(bool WithAsNoTracking = true);

        IQueryable<Employee> GetAllAsIQueryable();

        Employee? GetById(int id);

        void Add(Employee entity);

        void Update(Employee entity);

        void Delete(Employee entity);
    }
}

using LinkDev.IKEA.DAL.Entities.Employee;

namespace LinkDev.IKEA.DAL.Persistance.Repositories.Employees
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll(bool WithAsNoTracking = true);

        IQueryable<Employee> GetAllAsIQueryable();

        Employee? GetById(int id);

        int Add(Employee entity);

        int Update(Employee entity);

        int Delete(Employee entity);
    }
}

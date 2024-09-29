using LinkDev.IKEA.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.Repositories._Generic
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        Task<IEnumerable<T>> GetAllAsync(bool WithAsNoTracking = true);
        
        IQueryable<T> GetAllAsIQueryable();

        Task<T?> GetByIdAsync(int id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}

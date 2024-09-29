using LinkDev.IKEA.DAL.Entities;
using LinkDev.IKEA.DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.DAL.Persistance.Repositories._Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase 
    {
        private protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext) // Ask CLR For Object From ApplicationDbContext
        {
            _dbContext = dbContext;
        }

        public void Add(T entity)
        {
             _dbContext.Set<T>().Add(entity);
   
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);

        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            _dbContext.Set<T>().Update(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool WithAsNoTracking = true)
        {
            if (WithAsNoTracking)
                return await _dbContext.Set<T>().Where(X => !X.IsDeleted).AsNoTracking().ToListAsync();

            return await _dbContext.Set<T>().Where(X => !X.IsDeleted).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbContext.FindAsync<T>(id);

            //return _dbContext.Departments.Find(id);


            //var T = _dbContext.Departments.Local.FirstOrDefault(D => D.Id == id);

            //if(T is null)
            //    T = _dbContext.Departments.FirstOrDefault(D => D.Id == id);

            //return T;  
        }

        public IQueryable<T> GetAllAsIQueryable()
        {
            return _dbContext.Set<T>();
        }
    }
}

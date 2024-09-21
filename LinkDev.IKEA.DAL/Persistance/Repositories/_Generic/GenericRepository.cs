using LinkDev.IKEA.DAL.Entities;
using LinkDev.IKEA.DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.DAL.Persistance.Repositories._Generic
{
    public class GenericRepository<T> where T : ModelBase 
    {
        private protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext) // Ask CLR For Object From ApplicationDbContext
        {
            _dbContext = dbContext;
        }

        public int Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(T entity)
        {
            entity.IsDeleted = true;
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll(bool WithAsNoTracking = true)
        {
            if (WithAsNoTracking)
                return _dbContext.Set<T>().AsNoTracking().ToList();

            return _dbContext.Set<T>().ToList();
        }

        public T? GetById(int id)
        {
            return _dbContext.Find<T>(id);

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

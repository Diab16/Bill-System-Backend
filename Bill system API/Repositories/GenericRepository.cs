using Bill_system_API.IRepositories;
using Bill_system_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Bill_system_API.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private ApplicationDbContext context;
        private DbSet<T> dbSet;
        public GenericRepository(ApplicationDbContext _context)
        {
            context = _context;
            dbSet=_context.Set<T>();
        }
        public void add(T entity)
        {
            dbSet.Add(entity);
        }

        public void delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T getById(int id)
        {
            return dbSet.Find(id);
        }

        public void save()
        {
            context.SaveChanges();
        }

        public void update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}

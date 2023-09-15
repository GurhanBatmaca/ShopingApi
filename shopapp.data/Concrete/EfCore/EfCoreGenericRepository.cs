using Microsoft.EntityFrameworkCore;
using shopapp.data.Abstract;

namespace shopapp.data.Concrete.EfCore
{
    public class EfCoreGenericRepository<T> : IRepository<T>

        where T : class
    {
        protected readonly DbContext context;
        public EfCoreGenericRepository(DbContext _context)
        {
            context = _context;
        }

        public async Task<int> CountAsync()
        {
            return await context.Set<T>().CountAsync();
        }


        public async Task CreateAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();

        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges(); 
        }

    }
}
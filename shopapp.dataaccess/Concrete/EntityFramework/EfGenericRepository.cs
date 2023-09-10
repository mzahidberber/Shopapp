using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.Entity.Abstract;
using shopapp.core.Reflection;
using System.Linq.Expressions;

namespace shopapp.dataaccess.Concrete.EntityFramework
{
    public class EfGenericRepository<T> : IGenericRepository<T> where T : class, IEntity, new()
    {
        protected readonly ShopContext _context;
        protected readonly DbSet<T> _dbSet;
        public EfGenericRepository(ShopContext context)
        {
            //Console.WriteLine($"create {this.ToString()}");
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public bool Commit(bool state = true)
        {
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> CommitAsync(bool state = true)
        {
            await _context.SaveChangesAsync();
            return true;
        }

        public void Dispose()
        {
            //Console.WriteLine($"dispose {this.ToString()}");
            _context.Dispose();
        }
        //public override string ToString()
        //{
        //    return this.GetType().Name;
        //}
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }


        public IQueryable<T> GetAll(Expression<Func<T, bool>>? filter = null)
        {
            if (filter != null) return _dbSet.Where(filter).AsQueryable();
            return _dbSet.AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            return entity;
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> filter)
        {
            return _dbSet.Where(filter);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}

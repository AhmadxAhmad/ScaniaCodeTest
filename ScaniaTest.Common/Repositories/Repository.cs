using Microsoft.EntityFrameworkCore;
using ScaniaTest.Common.Data;
using System.Linq.Expressions;

namespace ScaniaTest.Common.Repositories
{
    public class Repository<db,T> : IRepository<db,T> where T : class where db : DbContext
    {

        private readonly db _context;

        public Repository(db db)
        {
            this._context = db;
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();
                if (includes != null && includes.Length > 0)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(GetAllAsync)}<{typeof(T).Name}> => {ex.Message}");
            }
        }
      

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        public async Task<IEnumerable<T>> GetAllByAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();
                if (includes != null && includes.Length > 0)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }
                return await query.Where(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(GetAllByAsync)}<{typeof(T).Name}> => {ex.Message}");
            }
          
        }
        public async Task CreateAsync(T entity)
        {
            

            try
            {
                if (entity is null) throw new ArgumentException(nameof(entity));
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(CreateAsync)}<{typeof(T).Name}> => {ex.Message}");
            }
        }

        public async Task UpdateAsync(T entity)
        {
           
            try
            {
                if (entity is null) throw new ArgumentException(nameof(entity));
                _context.Set<T>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(UpdateAsync)}<{typeof(T).Name}> => {ex.Message}");
            }
        }
        public async Task RemoveAsync(T entity)
        {
            try
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(RemoveAsync)}<{typeof(T).Name}> => {ex.Message}");
            }
        }

        public async Task<T> GetOneBy(Expression<Func<T, bool>> filter)
        {
            try
            {
                return await _context.Set<T>().Where(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(GetOneBy)}<{typeof(T).Name}> => {ex.Message}");
            }

        }
    }
}
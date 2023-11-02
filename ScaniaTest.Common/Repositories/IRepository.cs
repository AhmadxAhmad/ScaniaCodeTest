using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ScaniaTest.Common.Repositories
{
    public interface IRepository<db,T> where T : class where db : DbContext
    {
        Task CreateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllByAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        Task<T> GetOneBy(Expression<Func<T, bool>> filter);
        Task RemoveAsync(T entity);
        Task UpdateAsync(T entity);
        Task SaveChangesAsync();
    }
}
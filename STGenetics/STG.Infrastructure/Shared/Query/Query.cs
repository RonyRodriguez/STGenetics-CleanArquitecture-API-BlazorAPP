
using Microsoft.EntityFrameworkCore;
using STG.Infrastructure.Databse;
using System.Linq.Expressions;

namespace STG.Infrastructure.Shared.Query
{
    public class Query<T> : IQuery<T> where T : class
    {

        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Query(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null)
        {
            var result = _context.Set<T>().AsNoTracking();
            if (expression != null)
            {
                result = result.Where(expression);
            }
            return await Task.FromResult(result);
        }

        public Task<T?> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}

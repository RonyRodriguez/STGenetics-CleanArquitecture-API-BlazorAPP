using System.Linq.Expressions;

namespace STG.Infrastructure.Shared.Query
{
    public interface IQuery<T> where T : class
    {
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null);

        Task<T?> GetByIdAsync(long id);
    }
}

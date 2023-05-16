namespace STG.Infrastructure.Shared.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}

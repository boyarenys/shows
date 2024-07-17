using System.Linq.Expressions;

namespace Domain.Interface
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task SaveChangesAsync();
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
    }
}

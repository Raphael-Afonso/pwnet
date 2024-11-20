using PwNet.Domain.Entities;
using System.Linq.Expressions;

namespace PwNet.Application.Interfaces.Infra.Persistence
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAsync(T entity, CancellationToken cancellationToken);
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> whereParam, CancellationToken cancellationToken);
        Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken);
        Task<T?> GetFirstByAction(Expression<Func<T, bool>> whereParam, CancellationToken cancellationToken);
        Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task UpdateAsync(T entity, CancellationToken cancellationToken);
    }
}

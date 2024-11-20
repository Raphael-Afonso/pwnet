using Microsoft.EntityFrameworkCore;
using PwNet.Application.Interfaces.Infra.Persistence;
using PwNet.Domain.Entities;
using PwNet.Infra.Persistence.Context;
using System.Linq.Expressions;

namespace PwNet.Infra.Persistence.Repositories
{
    public class GenericRepository<T>(SqlContext dbContext) : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly SqlContext _dbContext = dbContext;

        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public virtual Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> whereParam, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<T>().Where(whereParam).ToListAsync(cancellationToken);
        }

        public virtual async Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<T>().FindAsync(id, cancellationToken);
        }

        public virtual async Task<T?> GetFirstByAction(Expression<Func<T, bool>> whereParam, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<T>().Where(whereParam).FirstOrDefaultAsync(cancellationToken);
        }

        public virtual Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            var trackedEntity = await _dbContext.Set<T>().FindAsync(entity.Id, cancellationToken)
                ?? throw new InvalidOperationException("Entity not found in the database.");

            _dbContext.Entry(trackedEntity).CurrentValues.SetValues(entity);

            _dbContext.Entry(trackedEntity).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PwNet.Domain.Entities;

namespace PwNet.Infra.Persistence.Interceptors
{
    internal class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if (context == null) return await base.SavingChangesAsync(eventData, result, cancellationToken);

            var now = DateTime.Now;

            foreach (var entry in context.ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.InsertedAt = now;
                        break;

                    case EntityState.Modified:
                        entry.Property("InsertedAt").IsModified = false;
                        entry.Entity.UpdatedAt = now;
                        break;
                }
            }

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}

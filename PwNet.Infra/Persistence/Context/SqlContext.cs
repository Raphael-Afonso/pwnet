using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PwNet.Common.Configurations;
using PwNet.Infra.Persistence.EntityConfigurations;
using PwNet.Infra.Persistence.Interceptors;

namespace PwNet.Infra.Persistence.Context
{
    public class SqlContext(IOptions<DatabaseConfig> options) : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(options.Value.ConnectionString);

            optionsBuilder.AddInterceptors(new AuditableEntityInterceptor());

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlayerConfigurations());
            modelBuilder.ApplyConfiguration(new IpBlockedConfigurations());

            base.OnModelCreating(modelBuilder);
        }
    }
}

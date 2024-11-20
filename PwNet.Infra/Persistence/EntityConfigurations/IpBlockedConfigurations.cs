using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PwNet.Domain.Entities;

namespace PwNet.Infra.Persistence.EntityConfigurations
{
    internal class IpBlockedConfigurations : IEntityTypeConfiguration<IpBlocked>
    {
        public void Configure(EntityTypeBuilder<IpBlocked> builder)
        {
            builder
                .ToTable("IpsBlocked")
                .HasKey(m => m.Id);

            builder
                .HasIndex(m => m.Id)
                .IsUnique();

            builder
                .Property(m => m.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasIndex(m => m.IpAddress)
                .IsUnique();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PwNet.Domain.Entities;

namespace PwNet.Infra.Persistence.EntityConfigurations
{
    internal class PlayerConfigurations : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder
                .ToTable("Players")
                .HasKey(m => m.Id);

            builder
                .HasIndex(m => m.Id)
                .IsUnique();

            builder
                .Property(m => m.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasIndex(m => m.Username)
                .IsUnique();
        }
    }
}

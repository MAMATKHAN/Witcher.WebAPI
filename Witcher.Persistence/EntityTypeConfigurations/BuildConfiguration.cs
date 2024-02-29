using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Witcher.Domain;

namespace Witcher.Persistence.EntityTypeConfigurations
{
    public class BuildConfiguration : IEntityTypeConfiguration<Build>
    {
        public void Configure(EntityTypeBuilder<Build> builder)
        {
            builder.HasKey(b => b.Id);
            builder.HasIndex(b => b.Id).IsUnique();
            builder.Property(b => b.Name).HasMaxLength(100);
        }
    }
}

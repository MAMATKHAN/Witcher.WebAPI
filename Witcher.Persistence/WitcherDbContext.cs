using Microsoft.EntityFrameworkCore;
using Witcher.Application.Interfaces;
using Witcher.Domain;
using Witcher.Persistence.EntityTypeConfigurations;

namespace Witcher.Persistence
{
    public class WitcherDbContext : DbContext, IWitcherDbContext
    {
        public DbSet<Build> Builds { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentCategory> EquipmentCategories { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<Domain.Environment> Environments { get; set; }
        public DbSet<EnvironmentCategory> EnvironmentsCategories { get; set; }

        public WitcherDbContext(DbContextOptions<WitcherDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BuildConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Witcher.Domain;

namespace Witcher.Application.Interfaces
{
    public interface IWitcherDbContext
    {
        DbSet<Build> Builds { get; set; }
        DbSet<Equipment> Equipments { get; set; }
        DbSet<EquipmentCategory> EquipmentCategories { get; set; }
        DbSet<EquipmentType> EquipmentTypes { get; set; }
        DbSet<Domain.Environment> Environments { get; set; }
        DbSet<EnvironmentCategory> EnvironmentsCategories { get; set; }
        //DbSet<Comment> Comments { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using Witcher.Application.Common.Exceptions;
using Witcher.Application.Interfaces;
using Witcher.Domain;

namespace Witcher.Application.Equipments.Commands.UpdateEquipment
{
    public class UpdateEquipmentCommandHandler : IRequestHandler<UpdateEquipmentCommand, string?>
    {
        private readonly IWitcherDbContext _dbContext;

        public UpdateEquipmentCommandHandler(IWitcherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string?> Handle(UpdateEquipmentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Equipments
                .FirstOrDefaultAsync(equipment => equipment.Id == request.Id, cancellationToken);
            var equipmentCategory = await _dbContext.EquipmentCategories
                .FirstOrDefaultAsync(category => category.Id == request.CategoryId, cancellationToken);
            var equipmentType = await _dbContext.EquipmentTypes
                .FirstOrDefaultAsync(type => type.Id == request.TypeId, cancellationToken);

            if (entity == null) throw new NotFoundException(nameof(Equipment), request.Id);
            if (equipmentCategory == null) throw new NotFoundException(nameof(EquipmentCategory), request.CategoryId);
            if (equipmentType == null) throw new NotFoundException(nameof(EquipmentType), request.TypeId);

            var oldFileName = entity.ImageName;

            entity.Name = request.Name;
            entity.Text = request.Text;
            entity.ImageName = request.ImageName;
            entity.ImageSource = request.ImageSource;
            entity.Damage = request.Damage;
            entity.Armor = request.Armor;
            entity.Effect = request.Effect;
            entity.Type = equipmentType;
            entity.Category = equipmentCategory;
            //entity.Builds = await FindBuildsAsync(request.BuildsId, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return oldFileName;
        }

        //private async Task<List<Build>?> FindBuildsAsync(List<Guid>? buildIds, CancellationToken cancellationToken)
        //{
        //    if (buildIds == null) return null;

        //    var builds = new List<Build>();
        //    Build? entity = null;
        //    foreach (var buildId in buildIds)
        //    {
        //        entity = await _dbContext.Builds.FirstOrDefaultAsync(build => build.Id == buildId, cancellationToken);
        //        if (entity == null) throw new NotFoundException(nameof(Build), buildId);
        //        builds.Add(entity);
        //    }
        //    return builds;
        //}
    }
}

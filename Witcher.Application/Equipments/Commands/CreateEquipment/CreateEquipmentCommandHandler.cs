using MediatR;
using Microsoft.EntityFrameworkCore;
using Witcher.Application.Interfaces;
using Witcher.Domain;
using Witcher.Application.Common.Exceptions;

namespace Witcher.Application.Equipments.Commands.CreateEquipment
{
    public class CreateEquipmentCommandHandler : IRequestHandler<CreateEquipmentCommand, Guid>
    {
        private readonly IWitcherDbContext _dbContext;

        public CreateEquipmentCommandHandler(IWitcherDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Guid> Handle(CreateEquipmentCommand request, CancellationToken cancellationToken)
        {
            var equipmentCategory = await _dbContext.EquipmentCategories
                .FirstOrDefaultAsync(category => category.Id == request.CategoryId, cancellationToken);
            var equipmentType = await _dbContext.EquipmentTypes
                .FirstOrDefaultAsync(type => type.Id == request.TypeId, cancellationToken);

            if (equipmentCategory == null) throw new NotFoundException(nameof(EquipmentCategory), request.CategoryId);
            if (equipmentType == null) throw new NotFoundException(nameof(EquipmentType), request.TypeId);


            var equipment = new Equipment
            {
                Name = request.Name,
                Text = request.Text,
                ImageName = request.ImageName,
                ImageSource = request.ImageSource,
                Damage = request.Damage,
                Armor = request.Armor,
                Effect = request.Effect,
                Type = equipmentType,
                Category = equipmentCategory,
            };

            await _dbContext.Equipments.AddAsync(equipment, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return equipment.Id;
        }
    }
}

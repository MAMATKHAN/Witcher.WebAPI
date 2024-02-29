using MediatR;
using Microsoft.EntityFrameworkCore;
using Witcher.Application.Interfaces;
using Witcher.Domain;
using Witcher.Application.Common.Exceptions;

namespace Witcher.Application.EquipmentCategories.Commands.UpdateEquipmentCategory
{
    public class UpdateEquipmentCategoryCommandHandler : IRequestHandler<UpdateEquipmentCategoryCommand, Unit>
    {
        private readonly IWitcherDbContext _dbContext;

        public UpdateEquipmentCategoryCommandHandler(IWitcherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateEquipmentCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.EquipmentCategories
                .FirstOrDefaultAsync(equipmentCategory =>  equipmentCategory.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(nameof(EquipmentCategory), request.Id);

            entity.Name = request.Name;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

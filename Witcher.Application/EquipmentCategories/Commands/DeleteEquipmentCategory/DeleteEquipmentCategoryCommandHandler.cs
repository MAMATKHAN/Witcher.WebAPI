using MediatR;
using Microsoft.EntityFrameworkCore;
using Witcher.Application.Interfaces;
using Witcher.Domain;
using Witcher.Application.Common.Exceptions;

namespace Witcher.Application.EquipmentCategories.Commands.DeleteEquipmentCategory
{
    public class DeleteEquipmentCategoryCommandHandler : IRequestHandler<DeleteEquipmentCategoryCommand, Unit>
    {
        private readonly IWitcherDbContext _dbContext;

        public DeleteEquipmentCategoryCommandHandler(IWitcherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteEquipmentCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.EquipmentCategories
                .FirstOrDefaultAsync(equipmentCategory => equipmentCategory.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(nameof(EquipmentCategory), request.Id);

            _dbContext.EquipmentCategories.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

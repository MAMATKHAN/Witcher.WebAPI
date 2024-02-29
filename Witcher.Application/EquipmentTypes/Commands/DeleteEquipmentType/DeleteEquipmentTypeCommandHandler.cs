using MediatR;
using Microsoft.EntityFrameworkCore;
using Witcher.Application.Interfaces;
using Witcher.Domain;
using Witcher.Application.Common.Exceptions;

namespace Witcher.Application.EquipmentTypes.Commands.DeleteEquipmentType
{
    public class DeleteEquipmentTypeCommandHandler : IRequestHandler<DeleteEquipmentTypeCommand, Unit>
    {
        private readonly IWitcherDbContext _dbContext;

        public DeleteEquipmentTypeCommandHandler(IWitcherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteEquipmentTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.EquipmentTypes
                .FirstOrDefaultAsync(equipmentType => equipmentType.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(nameof(EquipmentType), request.Id);

            _dbContext.EquipmentTypes.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

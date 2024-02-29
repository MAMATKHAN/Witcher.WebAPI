using MediatR;
using Microsoft.EntityFrameworkCore;
using Witcher.Application.Interfaces;
using Witcher.Domain;
using Witcher.Application.Common.Exceptions;

namespace Witcher.Application.EquipmentTypes.Commands.UpdateEquipmentType
{
    public class UpdateEquipmentTypeCommandHandler : IRequestHandler<UpdateEquipmentTypeCommand, Unit>
    {
        private readonly IWitcherDbContext _dbContext;

        public UpdateEquipmentTypeCommandHandler(IWitcherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateEquipmentTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.EquipmentTypes
                .FirstOrDefaultAsync(equipmentType =>  equipmentType.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(nameof(EquipmentType), request.Id);

            entity.Name = request.Name;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

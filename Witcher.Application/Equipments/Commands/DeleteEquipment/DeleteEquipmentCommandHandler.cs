using MediatR;
using Microsoft.EntityFrameworkCore;
using Witcher.Application.Interfaces;
using Witcher.Application.Common.Exceptions;
using Witcher.Domain;

namespace Witcher.Application.Equipments.Commands.DeleteEquipment
{
    public class DeleteEquipmentCommandHandler : IRequestHandler<DeleteEquipmentCommand, string?>
    {
        private readonly IWitcherDbContext _dbContext;

        public DeleteEquipmentCommandHandler(IWitcherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string?> Handle(DeleteEquipmentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Equipments
                .FirstOrDefaultAsync(equipment => equipment.Id == request.Id, cancellationToken);
            if(entity == null) throw new NotFoundException(nameof(Equipment), request.Id);

            _dbContext.Equipments.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.ImageName;
        }
    }
}

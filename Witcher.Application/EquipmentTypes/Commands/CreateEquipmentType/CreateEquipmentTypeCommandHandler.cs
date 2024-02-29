using MediatR;
using Witcher.Application.Interfaces;
using Witcher.Domain;

namespace Witcher.Application.EquipmentTypes.Commands.CreateEquipmentType
{
    public class CreateEquipmentTypeCommandHandler : IRequestHandler<CreateEquipmentTypeCommand, Guid>
    {
        private readonly IWitcherDbContext _dbContext;

        public CreateEquipmentTypeCommandHandler(IWitcherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateEquipmentTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = new EquipmentType { Name = request.Name };

            await _dbContext.EquipmentTypes.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}

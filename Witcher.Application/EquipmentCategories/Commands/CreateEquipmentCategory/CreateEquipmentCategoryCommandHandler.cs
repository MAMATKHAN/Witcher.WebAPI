using MediatR;
using Witcher.Application.Interfaces;
using Witcher.Domain;

namespace Witcher.Application.EquipmentCategories.Commands.CreateEquipmentCategory
{
    public class CreateEquipmentCategoryCommandHandler : IRequestHandler<CreateEquipmentCategoryCommand, Guid>
    {
        private readonly IWitcherDbContext _dbContext;

        public CreateEquipmentCategoryCommandHandler(IWitcherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateEquipmentCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = new EquipmentCategory { Name = request.Name };

            await _dbContext.EquipmentCategories.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}

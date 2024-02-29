using MediatR;
using Witcher.Application.Interfaces;
using Witcher.Domain;

namespace Witcher.Application.EnvironmentCategories.Commands.CreateEnvironmentCategory
{
    public class CreateEnvironmentCategoryCommandHandler : IRequestHandler<CreateEnvironmentCategoryCommand, Guid>
    {
        private readonly IWitcherDbContext _dbContext;

        public CreateEnvironmentCategoryCommandHandler(IWitcherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateEnvironmentCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = new EnvironmentCategory { Name = request.Name };

            await _dbContext.EnvironmentsCategories.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}

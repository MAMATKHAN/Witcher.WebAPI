using MediatR;
using Microsoft.EntityFrameworkCore;
using Witcher.Application.Interfaces;
using Witcher.Domain;
using Witcher.Application.Common.Exceptions;

namespace Witcher.Application.EnvironmentCategories.Commands.UpdateEnvironmentCategory
{
    public class UpdateEnvironmentCategoryCommandHandler : IRequestHandler<UpdateEnvironmentCategoryCommand, Unit>
    {
        private readonly IWitcherDbContext _dbContext;

        public UpdateEnvironmentCategoryCommandHandler(IWitcherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateEnvironmentCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.EnvironmentsCategories
                .FirstOrDefaultAsync(environmentCategory =>  environmentCategory.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(nameof(EnvironmentCategory), request.Id);

            entity.Name = request.Name;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

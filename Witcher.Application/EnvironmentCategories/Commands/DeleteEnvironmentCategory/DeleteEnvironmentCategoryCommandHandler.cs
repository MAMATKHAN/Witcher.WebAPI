using MediatR;
using Microsoft.EntityFrameworkCore;
using Witcher.Application.Interfaces;
using Witcher.Domain;
using Witcher.Application.Common.Exceptions;

namespace Witcher.Application.EnvironmentCategories.Commands.DeleteEnvironmentCategory
{
    public class DeleteEnvironmentCategoryCommandHandler : IRequestHandler<DeleteEnvironmentCategoryCommand, Unit>
    {
        private readonly IWitcherDbContext _dbContext;

        public DeleteEnvironmentCategoryCommandHandler(IWitcherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteEnvironmentCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.EnvironmentsCategories
                .FirstOrDefaultAsync(environmentCategory =>  environmentCategory.Id == request.Id, cancellationToken);
            if (entity == null) throw new NotFoundException(nameof(EnvironmentCategory), request.Id);

            _dbContext.EnvironmentsCategories.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

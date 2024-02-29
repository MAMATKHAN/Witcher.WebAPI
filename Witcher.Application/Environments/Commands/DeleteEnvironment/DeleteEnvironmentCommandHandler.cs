using MediatR;
using Witcher.Application.Interfaces;
using Witcher.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Witcher.Application.Environments.Commands.DeleteEnvironment
{
    public class DeleteEnvironmentCommandHandler : IRequestHandler<DeleteEnvironmentCommand, string?>
    {
        private readonly IWitcherDbContext _dbContext;

        public DeleteEnvironmentCommandHandler(IWitcherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string?> Handle(DeleteEnvironmentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Environments
                .FirstOrDefaultAsync(environment =>  environment.Id == request.Id, cancellationToken);
            
            if (entity == null) throw new NotFoundException(nameof(Domain.Environment), request.Id);

            _dbContext.Environments.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.ImageName;
        }
    }
}

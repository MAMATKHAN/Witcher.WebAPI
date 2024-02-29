using MediatR;
using Microsoft.EntityFrameworkCore;
using Witcher.Application.Interfaces;
using Witcher.Domain;
using Witcher.Application.Common.Exceptions;

namespace Witcher.Application.Builds.Commands.DeleteBuild
{
    public class DeleteBuildCommandHandler : IRequestHandler<DeleteBuildCommand, string?>
    {
        private readonly IWitcherDbContext _dbContext;

        public DeleteBuildCommandHandler(IWitcherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string?> Handle(DeleteBuildCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Builds.FirstOrDefaultAsync(build => build.Id == request.Id, cancellationToken);
            if (entity == null) throw new NotFoundException(nameof(Build), request.Id);

            _dbContext.Builds.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.ImageName;
        }
    }
}

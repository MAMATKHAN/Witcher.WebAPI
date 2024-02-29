using MediatR;
using Microsoft.EntityFrameworkCore;
using Witcher.Application.Interfaces;
using Witcher.Domain;
using Witcher.Application.Common.Exceptions;

namespace Witcher.Application.Builds.Commands.UpdateBuild
{
    public class UpdateBuildCommandHandler : IRequestHandler<UpdateBuildCommand, string?>
    {
        private readonly IWitcherDbContext _dbContext;

        public UpdateBuildCommandHandler(IWitcherDbContext context)
        {
            _dbContext = context;
        }

        public async Task<string?> Handle(UpdateBuildCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Builds.FirstOrDefaultAsync(build => build.Id == request.Id, cancellationToken);
            if (entity == null) throw new NotFoundException(nameof(Build), request.Id);

            var oldFileName = entity.ImageName;

            entity.Name = request.Name;
            entity.Text = request.Text;
            entity.ImageName = request.ImageName;
            entity.ImageSource = request.ImageSource;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return oldFileName;
        }
    }
}

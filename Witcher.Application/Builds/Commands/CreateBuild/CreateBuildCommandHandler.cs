using MediatR;
using Witcher.Application.Interfaces;
using Witcher.Domain;

namespace Witcher.Application.Builds.Commands.CreateBuild
{
    public class CreateBuildCommandHandler : IRequestHandler<CreateBuildCommand, Guid>
    {
        private readonly IWitcherDbContext _dbContext;

        public CreateBuildCommandHandler(IWitcherDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Guid> Handle(CreateBuildCommand request, CancellationToken cancellationToken)
        {
            var entity = new Build
            {
                Name = request.Name,
                Text = request.Text,
                ImageName = request.ImageName,
                ImageSource = request.ImageSource,
                CreatedDate = DateTime.Now
            };

            await _dbContext.Builds.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}

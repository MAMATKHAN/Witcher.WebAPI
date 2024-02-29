using MediatR;
using Microsoft.EntityFrameworkCore;
using Witcher.Application.Interfaces;
using Witcher.Application.Common.Exceptions;
using Witcher.Domain;

namespace Witcher.Application.Environments.Commands.UpdateEnvironment
{
    public class UpdateEnvironmentCommandHandler : IRequestHandler<UpdateEnvironmentCommand, string?>
    {
        private readonly IWitcherDbContext _dbContext;

        public UpdateEnvironmentCommandHandler(IWitcherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string?> Handle(UpdateEnvironmentCommand request, CancellationToken cancellationToken)
        {
            var environmentCategory = await _dbContext.EnvironmentsCategories
                .FirstOrDefaultAsync(category => category.Id == request.CategoryId, cancellationToken);
            var entity = await _dbContext.Environments
                .FirstOrDefaultAsync(environment => environment.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(nameof(Domain.Environment), request.Id);
            if (environmentCategory == null) new NotFoundException(nameof(EnvironmentCategory), request.CategoryId);

            var oldFileName = entity.ImageName;

            entity.Name = request.Name;
            entity.Text = request.Text;
            entity.Alias = request.Alias;
            entity.Race = request.Race;
            entity.Profession = request.Profession;
            entity.Nationality = request.Nationality;
            entity.ImageName = request.ImageName;
            entity.ImageSource = request.ImageSource;
            entity.Category = environmentCategory;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return oldFileName;
        }
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using Witcher.Application.Interfaces;
using Witcher.Domain;
using Witcher.Application.Common.Exceptions;

namespace Witcher.Application.Environments.Commands.CreateEnvironment
{
    public class CreateEnvironmentCommandHandler : IRequestHandler<CreateEnvironmentCommand, Guid>
    {
        private readonly IWitcherDbContext _dbContext;

        public CreateEnvironmentCommandHandler(IWitcherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateEnvironmentCommand request, CancellationToken cancellationToken)
        {
            var environmentCategory = await _dbContext.EnvironmentsCategories
                .FirstOrDefaultAsync(category => category.Id == request.CategoryId, cancellationToken);
            if (environmentCategory == null) throw new NotFoundException(nameof(EnvironmentCategory), request.CategoryId);

            var entity = new Domain.Environment
            {
                Name = request.Name,
                Text = request.Text,
                Alias = request.Alias,
                Race = request.Race,
                Profession = request.Profession,
                Nationality = request.Nationality,
                ImageName = request.ImageName,
                ImageSource = request.ImageSource,
                Category = environmentCategory
            };

            await _dbContext.Environments.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}

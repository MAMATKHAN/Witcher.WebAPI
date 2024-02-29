using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Witcher.Application.Interfaces;
using Witcher.Application.Common.Exceptions;

namespace Witcher.Application.Environments.Queries.GetEnvironmentDetails
{
    public class GetEnvironmentDetailsQueryHandler : IRequestHandler<GetEnvironmentDetailsQuery, EnvironmentVm>
    {
        private readonly IWitcherDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetEnvironmentDetailsQueryHandler(IWitcherDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<EnvironmentVm> Handle(GetEnvironmentDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Environments.Include(environment => environment.Category)
                .FirstOrDefaultAsync(environment =>  environment.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(nameof(Domain.Environment), request.Id);

            return _mapper.Map<EnvironmentVm>(entity);
        }
    }
}

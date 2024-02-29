using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Witcher.Application.Interfaces;
using AutoMapper.QueryableExtensions;

namespace Witcher.Application.Environments.Queries.GetEnvironmentList
{
    public class GetEnvironmentListQueryHandler : IRequestHandler<GetEnvironmentListQuery, EnvironmentListVm>
    {
        private readonly IWitcherDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetEnvironmentListQueryHandler(IWitcherDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<EnvironmentListVm> Handle(GetEnvironmentListQuery request, CancellationToken cancellationToken)
        {
            var entityQuery = await _dbContext.Environments
                .ProjectTo<EnvironmentLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new EnvironmentListVm { Environments = entityQuery };
        }
    }
}

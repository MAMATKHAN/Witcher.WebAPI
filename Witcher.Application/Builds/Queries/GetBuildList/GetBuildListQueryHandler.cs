using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Witcher.Application.Interfaces;

namespace Witcher.Application.Builds.Queries.GetBuildList
{
    public class GetBuildListQueryHandler : IRequestHandler<GetBuildListQuery, BuildListVm>
    {
        private readonly IWitcherDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBuildListQueryHandler(IWitcherDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<BuildListVm> Handle(GetBuildListQuery request, CancellationToken cancellationToken)
        {
            var entityQuery = await _dbContext.Builds
                .ProjectTo<BuildLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new BuildListVm { Builds = entityQuery };
        }
    }
}

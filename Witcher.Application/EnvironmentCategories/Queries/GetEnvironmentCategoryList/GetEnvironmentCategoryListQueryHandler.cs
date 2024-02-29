using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Witcher.Application.Interfaces;

namespace Witcher.Application.EnvironmentCategories.Queries.GetEnvironmentCategoryList
{
    public class GetEnvironmentCategoryListQueryHandler : IRequestHandler<GetEnvironmentCategoryListQuery, EnvironmentCategoryListVm>
    {
        private readonly IWitcherDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetEnvironmentCategoryListQueryHandler(IWitcherDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<EnvironmentCategoryListVm> Handle(GetEnvironmentCategoryListQuery request, CancellationToken cancellationToken)
        {
            var entityQuery = await _dbContext.EnvironmentsCategories
                .ProjectTo<EnvironmentCategoryLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new EnvironmentCategoryListVm { EnvironemntCategories = entityQuery };
        }
    }
}

using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Witcher.Application.Interfaces;

namespace Witcher.Application.EquipmentCategories.Queries.GetEquipmentCategoryList
{
    public class GetEquipmentCategoryListQueryHandler : IRequestHandler<GetEquipmentCategoryListQuery, EquipmentCategoryListVm>
    {
        private readonly IWitcherDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetEquipmentCategoryListQueryHandler(IWitcherDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<EquipmentCategoryListVm> Handle(GetEquipmentCategoryListQuery request, CancellationToken cancellationToken)
        {
            var entityQuery = await _dbContext.EquipmentCategories
                .ProjectTo<EquipmentCategoryLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new EquipmentCategoryListVm { EquipmentCategories = entityQuery };
        }
    }
}

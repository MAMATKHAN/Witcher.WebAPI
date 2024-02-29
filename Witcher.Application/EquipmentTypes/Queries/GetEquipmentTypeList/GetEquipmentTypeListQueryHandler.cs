using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Witcher.Application.Interfaces;

namespace Witcher.Application.EquipmentTypes.Queries.GetEquipmentTypeList
{
    public class GetEquipmentTypeListQueryHandler : IRequestHandler<GetEquipmentTypeListQuery, EquipmentTypeListVm>
    {
        private readonly IWitcherDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetEquipmentTypeListQueryHandler(IWitcherDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<EquipmentTypeListVm> Handle(GetEquipmentTypeListQuery request, CancellationToken cancellationToken)
        {
            var entityQuery = await _dbContext.EquipmentTypes
                .ProjectTo<EquipmentTypeLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new EquipmentTypeListVm { EquipmentTypes = entityQuery };
        }
    }
}

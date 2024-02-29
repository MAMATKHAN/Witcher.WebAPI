using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Witcher.Application.Interfaces;

namespace Witcher.Application.Equipments.Quieries.GetEquipmentList
{
    public class GetEquipmentListQueryHandler : IRequestHandler<GetEquipmentListQuery, EquipmentListVm>
    {
        private readonly IWitcherDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetEquipmentListQueryHandler(IWitcherDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<EquipmentListVm> Handle(GetEquipmentListQuery request, CancellationToken cancellationToken)
        {
            var equipmentQuery = await _dbContext.Equipments
                .ProjectTo<EquipmentLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new EquipmentListVm { Equipments = equipmentQuery };
        }
    }
}

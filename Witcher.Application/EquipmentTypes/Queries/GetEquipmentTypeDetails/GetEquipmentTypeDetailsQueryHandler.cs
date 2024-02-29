using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Witcher.Application.Interfaces;
using Witcher.Domain;
using Witcher.Application.Common.Exceptions;

namespace Witcher.Application.EquipmentTypes.Queries.GetEquipmentTypeDetails
{
    public class GetEquipmentTypeDetailsQueryHandler : IRequestHandler<GetEquipmentTypeDetailsQuery, EquipmentTypeVm>
    {
        private readonly IWitcherDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetEquipmentTypeDetailsQueryHandler(IWitcherDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<EquipmentTypeVm> Handle(GetEquipmentTypeDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.EquipmentTypes
                .FirstOrDefaultAsync(equipmentType =>  equipmentType.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(nameof(EquipmentType), request.Id);

            return _mapper.Map<EquipmentTypeVm>(entity);
        }
    }
}

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Witcher.Application.Interfaces;
using Witcher.Domain;
using Witcher.Application.Common.Exceptions;

namespace Witcher.Application.Equipments.Quieries.GetEquipmentDetails
{
    public class GetEquipmentDetailsQeryHandler : IRequestHandler<GetEquipmentDetailsQeury, EquipmentVm>
    {
        readonly IWitcherDbContext _dbContext;
        readonly IMapper _mapper;

        public GetEquipmentDetailsQeryHandler(IWitcherDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<EquipmentVm> Handle(GetEquipmentDetailsQeury request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Equipments.Include(equipment => equipment.Category)
                .Include(equipment => equipment.Type)
                .FirstOrDefaultAsync(equipment =>  equipment.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(nameof(Equipment), request.Id);

            return _mapper.Map<EquipmentVm>(entity);
        }        
    }
}

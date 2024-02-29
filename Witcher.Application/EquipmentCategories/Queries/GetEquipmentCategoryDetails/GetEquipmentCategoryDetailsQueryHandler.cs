using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Witcher.Application.Interfaces;
using Witcher.Domain;
using Witcher.Application.Common.Exceptions;

namespace Witcher.Application.EquipmentCategories.Queries.GetEquipmentCategoryDetails
{
    public class GetEquipmentCategoryDetailsQueryHandler : IRequestHandler<GetEquipmentCategoryDetailsQuery, EquipmentCategoryVm>
    {
        private readonly IWitcherDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetEquipmentCategoryDetailsQueryHandler(IMapper mapper, IWitcherDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<EquipmentCategoryVm> Handle(GetEquipmentCategoryDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.EquipmentCategories
                .FirstOrDefaultAsync(equipmentCategory => equipmentCategory.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(nameof(EquipmentCategory), request.Id);

            return _mapper.Map<EquipmentCategoryVm>(entity);
        }
    }
}

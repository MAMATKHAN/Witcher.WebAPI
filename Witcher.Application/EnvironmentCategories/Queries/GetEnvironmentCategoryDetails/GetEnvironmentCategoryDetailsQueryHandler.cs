using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Witcher.Application.Interfaces;
using Witcher.Domain;
using Witcher.Application.Common.Exceptions;

namespace Witcher.Application.EnvironmentCategories.Queries.GetEnvironmentCategoryDetails
{
    public class GetEnvironmentCategoryDetailsQueryHandler : IRequestHandler<GetEnvironmentCategoryDetailsQuery, EnvironmentCategoryVm>
    {
        private IWitcherDbContext _dbContext;
        private IMapper _mapper;

        public GetEnvironmentCategoryDetailsQueryHandler(IWitcherDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<EnvironmentCategoryVm> Handle(GetEnvironmentCategoryDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.EnvironmentsCategories
                .FirstOrDefaultAsync(environmentCategory => environmentCategory.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(nameof(EnvironmentCategory), request.Id);

            return _mapper.Map<EnvironmentCategoryVm>(entity);
        }
    }
}

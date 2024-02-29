using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Witcher.Application.Interfaces;
using Witcher.Domain;
using Witcher.Application.Common.Exceptions;

namespace Witcher.Application.Builds.Queries.GetBuildDetails
{
    public class GetBuildDetailsQueryHandler : IRequestHandler<GetBuildDetailsQuery, BuildVm>
    {
        private readonly IWitcherDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBuildDetailsQueryHandler(IWitcherDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<BuildVm> Handle(GetBuildDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Builds
                .FirstOrDefaultAsync(build => build.Id == request.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(nameof(Build), request.Id);

            return _mapper.Map<BuildVm>(entity);
        }
    }
}

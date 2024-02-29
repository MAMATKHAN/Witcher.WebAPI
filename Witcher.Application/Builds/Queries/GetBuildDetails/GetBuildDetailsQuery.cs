using MediatR;

namespace Witcher.Application.Builds.Queries.GetBuildDetails
{
    public class GetBuildDetailsQuery : IRequest<BuildVm>
    {
        public Guid Id { get; set; }
    }
}

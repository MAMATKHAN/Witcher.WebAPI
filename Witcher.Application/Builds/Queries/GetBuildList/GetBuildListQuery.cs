using MediatR;

namespace Witcher.Application.Builds.Queries.GetBuildList
{
    public class GetBuildListQuery : IRequest<BuildListVm> { }
}

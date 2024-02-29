using MediatR;

namespace Witcher.Application.Environments.Queries.GetEnvironmentList
{
    public class GetEnvironmentListQuery : IRequest<EnvironmentListVm> { }
}

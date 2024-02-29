using MediatR;

namespace Witcher.Application.Environments.Queries.GetEnvironmentDetails
{
    public class GetEnvironmentDetailsQuery : IRequest<EnvironmentVm>
    {
        public Guid Id { get; set; }
    }
}

using MediatR;

namespace Witcher.Application.Environments.Commands.DeleteEnvironment
{
    public class DeleteEnvironmentCommand : IRequest<string?>
    {
        public Guid Id { get; set; }
    }
}

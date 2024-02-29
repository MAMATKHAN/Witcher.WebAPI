using MediatR;

namespace Witcher.Application.Builds.Commands.DeleteBuild
{
    public class DeleteBuildCommand : IRequest<string?>
    {
        public Guid Id { get; set; }
    }
}

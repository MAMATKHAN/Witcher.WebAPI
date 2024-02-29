using MediatR;

namespace Witcher.Application.Builds.Commands.CreateBuild
{
    public class CreateBuildCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string? Text { get; set; }
        public string? ImageName { get; set; }
        public string? ImageSource { get; set; }
    }
}

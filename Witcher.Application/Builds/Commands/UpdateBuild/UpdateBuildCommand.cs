using MediatR;

namespace Witcher.Application.Builds.Commands.UpdateBuild
{
    public class UpdateBuildCommand : IRequest<string?>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Text { get; set; }
        public string? ImageName { get; set; }
        public string? ImageSource { get; set; }
    }
}

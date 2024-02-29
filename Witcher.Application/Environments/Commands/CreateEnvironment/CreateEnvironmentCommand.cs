using MediatR;

namespace Witcher.Application.Environments.Commands.CreateEnvironment
{
    public class CreateEnvironmentCommand : IRequest<Guid>
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string? Text { get; set; }
        public string? Alias { get; set; }
        public string? Race { get; set; }
        public string? Profession { get; set; }
        public string? Nationality { get; set; }
        public string? ImageName { get; set; }
        public string? ImageSource { get; set; }
    }
}

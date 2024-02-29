using MediatR;

namespace Witcher.Application.Environments.Commands.UpdateEnvironment
{
    public class UpdateEnvironmentCommand : IRequest<string?>
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string? Alias { get; set; }
        public string? Race { get; set; }
        public string? Profession { get; set; }
        public string? Nationality { get; set; }
        public string? Text { get; set; }
        public string? ImageName { get; set; }
        public string? ImageSource { get; set; }
    }
}

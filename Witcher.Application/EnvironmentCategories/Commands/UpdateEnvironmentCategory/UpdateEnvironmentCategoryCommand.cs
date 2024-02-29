using MediatR;

namespace Witcher.Application.EnvironmentCategories.Commands.UpdateEnvironmentCategory
{
    public class UpdateEnvironmentCategoryCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}

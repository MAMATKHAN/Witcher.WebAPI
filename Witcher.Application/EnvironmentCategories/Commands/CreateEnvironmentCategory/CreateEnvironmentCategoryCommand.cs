using MediatR;

namespace Witcher.Application.EnvironmentCategories.Commands.CreateEnvironmentCategory
{
    public class CreateEnvironmentCategoryCommand : IRequest<Guid>
    {
        public string Name { get; set; }
    }
}

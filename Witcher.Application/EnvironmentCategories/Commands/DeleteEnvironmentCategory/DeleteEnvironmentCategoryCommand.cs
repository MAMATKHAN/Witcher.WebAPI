using MediatR;

namespace Witcher.Application.EnvironmentCategories.Commands.DeleteEnvironmentCategory
{
    public class DeleteEnvironmentCategoryCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}

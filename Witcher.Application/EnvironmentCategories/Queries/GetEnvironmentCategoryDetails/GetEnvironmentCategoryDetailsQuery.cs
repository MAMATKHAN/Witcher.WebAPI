using MediatR;

namespace Witcher.Application.EnvironmentCategories.Queries.GetEnvironmentCategoryDetails
{
    public class GetEnvironmentCategoryDetailsQuery : IRequest<EnvironmentCategoryVm>
    {
        public Guid Id { get; set; }
    }
}

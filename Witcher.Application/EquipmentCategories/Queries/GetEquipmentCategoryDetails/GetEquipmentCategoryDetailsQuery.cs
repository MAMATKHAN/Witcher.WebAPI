using MediatR;

namespace Witcher.Application.EquipmentCategories.Queries.GetEquipmentCategoryDetails
{
    public class GetEquipmentCategoryDetailsQuery : IRequest<EquipmentCategoryVm>
    {
        public Guid Id { get; set; }
    }
}

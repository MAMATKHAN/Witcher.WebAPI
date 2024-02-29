using MediatR;

namespace Witcher.Application.EquipmentCategories.Commands.UpdateEquipmentCategory
{
    public class UpdateEquipmentCategoryCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}

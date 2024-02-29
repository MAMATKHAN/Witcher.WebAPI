using MediatR;

namespace Witcher.Application.EquipmentCategories.Commands.DeleteEquipmentCategory
{
    public class DeleteEquipmentCategoryCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}

using MediatR;

namespace Witcher.Application.EquipmentCategories.Commands.CreateEquipmentCategory
{
    public class CreateEquipmentCategoryCommand : IRequest<Guid>
    {
        public string Name { get; set; }
    }
}

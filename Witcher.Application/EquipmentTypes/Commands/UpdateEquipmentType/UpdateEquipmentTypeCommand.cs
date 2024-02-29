using MediatR;

namespace Witcher.Application.EquipmentTypes.Commands.UpdateEquipmentType
{
    public class UpdateEquipmentTypeCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}

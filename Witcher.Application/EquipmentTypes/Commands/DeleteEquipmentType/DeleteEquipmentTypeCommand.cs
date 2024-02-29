using MediatR;

namespace Witcher.Application.EquipmentTypes.Commands.DeleteEquipmentType
{
    public class DeleteEquipmentTypeCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}

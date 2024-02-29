using MediatR;

namespace Witcher.Application.EquipmentTypes.Commands.CreateEquipmentType
{
    public class CreateEquipmentTypeCommand : IRequest<Guid>
    {
        public string Name { get; set; }
    }
}

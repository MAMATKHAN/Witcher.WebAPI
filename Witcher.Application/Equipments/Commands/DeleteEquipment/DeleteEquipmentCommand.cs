using MediatR;

namespace Witcher.Application.Equipments.Commands.DeleteEquipment
{
    public class DeleteEquipmentCommand : IRequest<string?>
    {
        public Guid Id { get; set; }
    }
}

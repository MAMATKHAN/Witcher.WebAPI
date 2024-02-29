using MediatR;

namespace Witcher.Application.Equipments.Quieries.GetEquipmentDetails
{
    public class GetEquipmentDetailsQeury : IRequest<EquipmentVm>
    {
        public Guid Id { get; set; }
    }
}

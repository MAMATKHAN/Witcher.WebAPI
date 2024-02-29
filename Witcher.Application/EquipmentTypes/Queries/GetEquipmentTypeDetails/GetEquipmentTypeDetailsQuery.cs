using MediatR;

namespace Witcher.Application.EquipmentTypes.Queries.GetEquipmentTypeDetails
{
    public class GetEquipmentTypeDetailsQuery : IRequest<EquipmentTypeVm>
    {
        public Guid Id { get; set; }
    }
}

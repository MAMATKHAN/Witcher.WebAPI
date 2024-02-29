using MediatR;

namespace Witcher.Application.Equipments.Commands.CreateEquipment
{
    public class CreateEquipmentCommand : IRequest<Guid>
    {
        public Guid TypeId { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string? Text { get; set; }
        public string? ImageName { get; set; }
        public string? ImageSource { get; set; }
        public int? Armor { get; set; }
        public int? Damage { get; set; }
        public string? Effect { get; set; }
        //public List<Guid> BuildsId { get; set; }
    }
}

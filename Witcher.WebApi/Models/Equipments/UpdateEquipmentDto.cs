using AutoMapper;
using Witcher.Application.Common.Mappings;
using Witcher.Application.Equipments.Commands.UpdateEquipment;

namespace Witcher.WebApi.Models.Equipments
{
    public class UpdateEquipmentDto : IMapWith<UpdateEquipmentCommand>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Text { get; set; }
        public int? Damage { get; set; }
        public int? Armor { get; set; }
        public string? Effect { get; set; }
        public Guid? TypeId { get; set; }
        public Guid? CategoryId { get; set; }
        public IFormFile? ImageFile { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateEquipmentDto, UpdateEquipmentCommand>()
                .ForMember(equipmentCommand => equipmentCommand.Id,
                    opt => opt.MapFrom(equipmentDto => equipmentDto.Id))
                .ForMember(equipmentCommand => equipmentCommand.Text,
                    opt => opt.MapFrom(equipmentDto => equipmentDto.Text))
                .ForMember(equipmentCommand => equipmentCommand.Damage,
                    opt => opt.MapFrom(equipmentDto => equipmentDto.Damage))
                .ForMember(equipmentCommand => equipmentCommand.Armor,
                    opt => opt.MapFrom(equipmentDto => equipmentDto.Armor))
                .ForMember(equipmentCommand => equipmentCommand.Effect,
                    opt => opt.MapFrom(equipmentDto => equipmentDto.Effect))
                .ForMember(equipmentCommand => equipmentCommand.Name,
                    opt => opt.MapFrom(equipmentDto => equipmentDto.Name))
                .ForMember(equipmentCommand => equipmentCommand.CategoryId,
                    opt => opt.MapFrom(equipmentDto => equipmentDto.CategoryId))
                .ForMember(equipmentCommand => equipmentCommand.TypeId,
                    opt => opt.MapFrom(equipmentDto => equipmentDto.TypeId));
        }
    }
}

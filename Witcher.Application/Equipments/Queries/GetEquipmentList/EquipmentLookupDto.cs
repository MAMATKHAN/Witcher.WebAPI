using AutoMapper;
using Witcher.Application.Common.Mappings;
using Witcher.Domain;

namespace Witcher.Application.Equipments.Quieries.GetEquipmentList
{
    public class EquipmentLookupDto : IMapWith<Equipment>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Text { get; set; }
        public string? ImageName { get; set; }
        public string? ImageSource { get; set; }
        public string? Category { get; set; }
        public string? Type { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Equipment, EquipmentLookupDto>()
                .ForMember(equipmentDto => equipmentDto.Id,
                    opt => opt.MapFrom(equipment => equipment.Id))
                .ForMember(equipmentDto => equipmentDto.Name,
                    opt => opt.MapFrom(equipment => equipment.Name))
                .ForMember(equipmentDto => equipmentDto.Text,
                    opt => opt.MapFrom(equipment => equipment.Text))
                .ForMember(equipmentDto => equipmentDto.ImageName,
                    opt => opt.MapFrom(equipment => equipment.ImageName))
                .ForMember(equipmentDto => equipmentDto.ImageSource,
                    opt => opt.MapFrom(equipment => equipment.ImageSource))
                .ForMember(equipmentVm => equipmentVm.Type,
                    opt => opt.MapFrom(equipment => (equipment.Type != null) ? equipment.Type.Name : null))
                .ForMember(equipmentVm => equipmentVm.Category,
                    opt => opt.MapFrom(equipment => (equipment.Category != null) ? equipment.Category.Name : null));
        }
    }
}

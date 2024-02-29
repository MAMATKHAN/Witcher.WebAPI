using AutoMapper;
using Witcher.Application.Common.Mappings;
using Witcher.Domain;

namespace Witcher.Application.Equipments.Quieries.GetEquipmentDetails
{
    public class EquipmentVm : IMapWith<Equipment>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Text { get; set; }
        public string? ImageName { get; set; }
        public string? ImageSource { get; set; }
        public int? Damage { get; set; }
        public int? Armor { get; set; }
        public string? Effect { get; set; }
        public string? Type { get; set; }
        public string? Category { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Equipment, EquipmentVm>()
                .ForMember(equipmentVm => equipmentVm.Id,
                    opt => opt.MapFrom(equipment => equipment.Id))
                .ForMember(equipmentVm => equipmentVm.Text,
                    opt => opt.MapFrom(equipment => equipment.Text))
                .ForMember(equipmentVm => equipmentVm.ImageName,
                    opt => opt.MapFrom(equipment => equipment.ImageName))
                .ForMember(equipmentVm => equipmentVm.Damage,
                    opt => opt.MapFrom(equipment => equipment.Damage))
                .ForMember(equipmentVm => equipmentVm.Armor,
                    opt => opt.MapFrom(equipment => equipment.Armor))
                .ForMember(equipmentVm => equipmentVm.Effect,
                    opt => opt.MapFrom(equipment => equipment.Effect))
                .ForMember(equipmentVm => equipmentVm.Type,
                    opt => opt.MapFrom(equipment => (equipment.Type != null)? equipment.Type.Name : null))
                .ForMember(equipmentVm => equipmentVm.Category,
                    opt => opt.MapFrom(equipment => (equipment.Category != null) ? equipment.Category.Name : null))
                .ForMember(equipmentVm => equipmentVm.Name,
                    opt => opt.MapFrom(equipment => equipment.Name))
                .ForMember(equipmentVm => equipmentVm.ImageSource,
                    opt => opt.MapFrom(equipment => equipment.ImageSource));
        }
    }
}

using AutoMapper;
using Witcher.Application.Common.Mappings;
using Witcher.Domain;

namespace Witcher.Application.EquipmentTypes.Queries.GetEquipmentTypeDetails
{
    public class EquipmentTypeVm : IMapWith<EquipmentType>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EquipmentType, EquipmentTypeVm>()
                .ForMember(equipmentTypeVm => equipmentTypeVm.Id,
                    opt => opt.MapFrom(equipmentType => equipmentType.Id))
                .ForMember(equipmentTypeVm => equipmentTypeVm.Name,
                    opt => opt.MapFrom(equipmentType => equipmentType.Name));
        }
    }
}

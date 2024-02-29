using AutoMapper;
using Witcher.Application.Common.Mappings;
using Witcher.Domain;

namespace Witcher.Application.EquipmentTypes.Queries.GetEquipmentTypeList
{
    public class EquipmentTypeLookupDto : IMapWith<EquipmentType>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<EquipmentType, EquipmentTypeLookupDto>()
                .ForMember(equipmentTypeDto => equipmentTypeDto.Id,
                    opt => opt.MapFrom(equipmentType => equipmentType.Id))
                .ForMember(equipmentTypeDto => equipmentTypeDto.Name,
                    opt => opt.MapFrom(equipmentType => equipmentType.Name));
        }
    }
}

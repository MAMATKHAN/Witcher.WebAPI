using AutoMapper;
using Witcher.Application.Common.Mappings;
using Witcher.Domain;

namespace Witcher.Application.EquipmentCategories.Queries.GetEquipmentCategoryList
{
    public class EquipmentCategoryLookupDto : IMapWith<EquipmentCategory>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EquipmentCategory, EquipmentCategoryLookupDto>()
                .ForMember(equipmentCategoryDto => equipmentCategoryDto.Id,
                    opt => opt.MapFrom(equipmentCategory => equipmentCategory.Id))
                .ForMember(equipmentCategoryDto => equipmentCategoryDto.Name,
                    opt => opt.MapFrom(equipmentCategory => equipmentCategory.Name));
        }
    }
}

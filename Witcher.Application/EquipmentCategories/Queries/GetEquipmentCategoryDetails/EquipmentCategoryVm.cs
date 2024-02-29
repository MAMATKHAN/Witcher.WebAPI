using AutoMapper;
using Witcher.Application.Common.Mappings;
using Witcher.Domain;

namespace Witcher.Application.EquipmentCategories.Queries.GetEquipmentCategoryDetails
{
    public class EquipmentCategoryVm : IMapWith<EquipmentCategory>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<EquipmentCategory, EquipmentCategoryVm>()
                .ForMember(equipmentCategoryVm => equipmentCategoryVm.Id,
                    opt => opt.MapFrom(equipmentCategory => equipmentCategory.Id))
                .ForMember(equipmentCategoryVm => equipmentCategoryVm.Name,
                    opt => opt.MapFrom(equipmentCategory => equipmentCategory.Name));
        }
    }
}

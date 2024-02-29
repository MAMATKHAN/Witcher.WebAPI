using AutoMapper;
using Witcher.Application.Common.Mappings;
using Witcher.Application.EquipmentCategories.Commands.CreateEquipmentCategory;

namespace Witcher.WebApi.Models.EqiupmentCategories
{
    public class CreateEquipmentCategoryDto : IMapWith<CreateEquipmentCategoryCommand>
    {
        public string Name { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateEquipmentCategoryDto, CreateEquipmentCategoryCommand>()
                .ForMember(equipmentCategoryCommand => equipmentCategoryCommand.Name,
                    opt => opt.MapFrom(equipmentCategoryDto => equipmentCategoryDto.Name));
        }
    }
}

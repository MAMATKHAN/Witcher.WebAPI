using AutoMapper;
using Witcher.Application.Common.Mappings;
using Witcher.Application.EquipmentCategories.Commands.UpdateEquipmentCategory;

namespace Witcher.WebApi.Models.EqiupmentCategories
{
    public class UpdateEquipmentCategoryDto : IMapWith<UpdateEquipmentCategoryCommand>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateEquipmentCategoryDto, UpdateEquipmentCategoryCommand>()
                .ForMember(equipmentCategoryCommand => equipmentCategoryCommand.Id,
                    opt => opt.MapFrom(equipmentCategoryDto => equipmentCategoryDto.Id))
                .ForMember(equipmentCategoryCommand => equipmentCategoryCommand.Name,
                    opt => opt.MapFrom(equipmentCategoryDto => equipmentCategoryDto.Name));
        }
    }
}

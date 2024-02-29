using AutoMapper;
using Witcher.Application.Common.Mappings;
using Witcher.Application.EquipmentTypes.Commands.UpdateEquipmentType;

namespace Witcher.WebApi.Models.EquipmentTypes
{
    public class UpdateEquipmentTypeDto : IMapWith<UpdateEquipmentTypeCommand>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateEquipmentTypeDto, UpdateEquipmentTypeCommand>()
                .ForMember(equipmentTypeCommand => equipmentTypeCommand.Id,
                    opt => opt.MapFrom(equipmentTypeDto => equipmentTypeDto.Id))
                .ForMember(equipmentTypeCommand => equipmentTypeCommand.Name,
                    opt => opt.MapFrom(equipmentTypeDto => equipmentTypeDto.Name));
        }
    }
}

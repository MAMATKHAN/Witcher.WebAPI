using AutoMapper;
using Witcher.Application.Common.Mappings;
using Witcher.Application.EquipmentTypes.Commands.CreateEquipmentType;

namespace Witcher.WebApi.Models.EquipmentTypes
{
    public class CreateEquipmentTypeDto : IMapWith<CreateEquipmentTypeCommand>
    {
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateEquipmentTypeDto, CreateEquipmentTypeCommand>()
                .ForMember(equipmentTypeCommand => equipmentTypeCommand.Name,
                    opt => opt.MapFrom(equipmentTypeDto => equipmentTypeDto.Name));
        }
    }
}

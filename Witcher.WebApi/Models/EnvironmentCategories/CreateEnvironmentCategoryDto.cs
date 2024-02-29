using AutoMapper;
using Witcher.Application.Common.Mappings;
using Witcher.Application.EnvironmentCategories.Commands.CreateEnvironmentCategory;

namespace Witcher.WebApi.Models.EnvironmentCategories
{
    public class CreateEnvironmentCategoryDto : IMapWith<CreateEnvironmentCategoryCommand>
    {
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateEnvironmentCategoryDto, CreateEnvironmentCategoryCommand>()
                .ForMember(environmentCategoryCommand => environmentCategoryCommand.Name,
                    opt => opt.MapFrom(environmentCategoryDto => environmentCategoryDto.Name));
        }
    }
}

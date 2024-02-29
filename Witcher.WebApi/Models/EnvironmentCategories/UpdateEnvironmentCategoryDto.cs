using AutoMapper;
using Witcher.Application.Common.Mappings;
using Witcher.Application.EnvironmentCategories.Commands.UpdateEnvironmentCategory;

namespace Witcher.WebApi.Models.EnvironmentCategories
{
    public class UpdateEnvironmentCategoryDto : IMapWith<UpdateEnvironmentCategoryCommand>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateEnvironmentCategoryDto, UpdateEnvironmentCategoryCommand>()
                .ForMember(environmentCategoryCommand => environmentCategoryCommand.Id,
                    opt => opt.MapFrom(environmentCategoryDto => environmentCategoryDto.Id))
                .ForMember(environmentCategoryCommand => environmentCategoryCommand.Name,
                    opt => opt.MapFrom(environmentCategoryDto => environmentCategoryDto.Name));
        }
    }
}

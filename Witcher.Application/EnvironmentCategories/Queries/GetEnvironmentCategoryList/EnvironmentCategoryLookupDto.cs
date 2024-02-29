using AutoMapper;
using Witcher.Application.Common.Mappings;
using Witcher.Domain;

namespace Witcher.Application.EnvironmentCategories.Queries.GetEnvironmentCategoryList
{
    public class EnvironmentCategoryLookupDto : IMapWith<EnvironmentCategory>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EnvironmentCategory, EnvironmentCategoryLookupDto>()
                .ForMember(environmentCategoryDto => environmentCategoryDto.Id,
                    opt => opt.MapFrom(environmentCategory => environmentCategory.Id))
                .ForMember(environmentCategoryDto => environmentCategoryDto.Name,
                    opt => opt.MapFrom(environmentCategory => environmentCategory.Name));
        }
    }
}

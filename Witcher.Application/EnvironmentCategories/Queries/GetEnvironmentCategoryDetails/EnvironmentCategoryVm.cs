using AutoMapper;
using Witcher.Application.Common.Mappings;
using Witcher.Domain;

namespace Witcher.Application.EnvironmentCategories.Queries.GetEnvironmentCategoryDetails
{
    public class EnvironmentCategoryVm : IMapWith<EnvironmentCategory>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public void Profile(Profile profile)
        {
            profile.CreateMap<EnvironmentCategory, EnvironmentCategoryVm>()
                .ForMember(environmentCategoryVm => environmentCategoryVm.Id,
                    opt => opt.MapFrom(environmentCategory => environmentCategory.Id))
                .ForMember(environmentCategoryVm => environmentCategoryVm.Name,
                    opt => opt.MapFrom(environmentCategory => environmentCategory.Name));
        }
    }
}

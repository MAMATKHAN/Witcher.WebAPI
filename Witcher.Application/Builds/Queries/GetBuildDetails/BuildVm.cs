using AutoMapper;
using Witcher.Application.Common.Mappings;
using Witcher.Domain;

namespace Witcher.Application.Builds.Queries.GetBuildDetails
{
    public class BuildVm : IMapWith<Build>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Text { get; set; }
        public string? ImageName { get; set; }
        public string? ImageSource { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Build,  BuildVm>()
                .ForMember(buildVm => buildVm.Id,
                    opt => opt.MapFrom(build => build.Id))
                .ForMember(buildVm => buildVm.Name,
                    opt => opt.MapFrom(build => build.Name))
                .ForMember(buildVm => buildVm.Text,
                    opt => opt.MapFrom(build => build.Text))
                .ForMember(buildVm => buildVm.ImageName,
                    opt => opt.MapFrom(build => build.ImageName))
                .ForMember(buildVm => buildVm.ImageSource,
                    opt => opt.MapFrom(build => build.ImageSource));
        }
    }
}

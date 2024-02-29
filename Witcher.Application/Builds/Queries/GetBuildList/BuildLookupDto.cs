using AutoMapper;
using Witcher.Application.Common.Mappings;
using Witcher.Domain;

namespace Witcher.Application.Builds.Queries.GetBuildList
{
    public class BuildLookupDto : IMapWith<Build>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Text { get; set; }
        public string? ImageName { get; set; }
        public string? ImageSource { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Build,  BuildLookupDto>()
                .ForMember(buildDto => buildDto.Id,
                    opt => opt.MapFrom(build => build.Id))
                .ForMember(buildDto => buildDto.Name,
                    opt => opt.MapFrom(build => build.Name))
                .ForMember(buildDto => buildDto.Text,
                    opt => opt.MapFrom(build => build.Text))
                .ForMember(buildDto => buildDto.ImageName,
                    opt => opt.MapFrom(build => build.ImageName))
                .ForMember(buildDto => buildDto.ImageSource,
                    opt => opt.MapFrom(build => build.ImageSource));
        }
    }
}

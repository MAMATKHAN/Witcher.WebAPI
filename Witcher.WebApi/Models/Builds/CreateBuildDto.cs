using AutoMapper;
using Witcher.Application.Builds.Commands.CreateBuild;
using Witcher.Application.Common.Mappings;

namespace Witcher.WebApi.Models.Builds
{
    public class CreateBuildDto : IMapWith<CreateBuildCommand>
    {
        public string Name { get; set; }
        public string? Text { get; set; }
        public IFormFile? ImageFile { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateBuildDto, CreateBuildCommand>()
                .ForMember(buildCommand => buildCommand.Name,
                    opt => opt.MapFrom(buildDto => buildDto.Name))
                .ForMember(buildCommand => buildCommand.Text,
                    opt => opt.MapFrom(buildDto => buildDto.Text));
        }
    }
}

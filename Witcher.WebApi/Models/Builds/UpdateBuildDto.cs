using AutoMapper;
using Witcher.Application.Builds.Commands.UpdateBuild;
using Witcher.Application.Common.Mappings;

namespace Witcher.WebApi.Models.Builds
{
    public class UpdateBuildDto : IMapWith<UpdateBuildCommand>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Text { get; set; }
        public IFormFile? ImageFile { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateBuildDto, UpdateBuildCommand>()
                .ForMember(buildCommand => buildCommand.Id,
                    opt => opt.MapFrom(buildDto => buildDto.Id))
                .ForMember(buildCommand => buildCommand.Name,
                    opt => opt.MapFrom(buildDto => buildDto.Name))
                .ForMember(buildCommand => buildCommand.Text,
                    opt => opt.MapFrom(buildDto => buildDto.Text));
        }
    }
}

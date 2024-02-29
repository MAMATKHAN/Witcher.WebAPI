using AutoMapper;
using Witcher.Application.Common.Mappings;
using Witcher.Application.Environments.Commands.CreateEnvironment;

namespace Witcher.WebApi.Models.Environments
{
    public class CreateEnvironmentDto : IMapWith<CreateEnvironmentCommand>
    {
        public string Name { get; set; }
        public string? Text { get; set; }
        public string? Alias { get; set; }
        public string? Race { get; set; }
        public string? Profession { get; set; }
        public string? Nationality { get; set; }
        public Guid CategoryId { get; set; }
        public IFormFile? ImageFile { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateEnvironmentDto, CreateEnvironmentCommand>()
                .ForMember(environmentCommand => environmentCommand.Name,
                    opt => opt.MapFrom(environmentDto => environmentDto.Name))
                .ForMember(environmentCommand => environmentCommand.Text,
                    opt => opt.MapFrom(environmentDto => environmentDto.Text))
                .ForMember(environmentCommand => environmentCommand.Alias,
                    opt => opt.MapFrom(environmentDto => environmentDto.Alias))
                .ForMember(environmentCommand => environmentCommand.Race,
                    opt => opt.MapFrom(environmentDto => environmentDto.Race))
                .ForMember(environmentCommand => environmentCommand.Profession,
                    opt => opt.MapFrom(environmentDto => environmentDto.Profession))
                .ForMember(environmentCommand => environmentCommand.Nationality,
                    opt => opt.MapFrom(environmentDto => environmentDto.Nationality))
                .ForMember(environmentCommand => environmentCommand.CategoryId,
                    opt => opt.MapFrom(environmentDto => environmentDto.CategoryId));
        }
    }
}

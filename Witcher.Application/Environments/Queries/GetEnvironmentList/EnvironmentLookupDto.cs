using AutoMapper;
using Witcher.Application.Common.Mappings;

namespace Witcher.Application.Environments.Queries.GetEnvironmentList
{
    public class EnvironmentLookupDto : IMapWith<Domain.Environment>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Text { get; set; }
        public string? ImageName { get; set; }
        public string? ImageSource { get; set; }
        public string Category { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Environment, EnvironmentLookupDto>()
                .ForMember(environmentDto => environmentDto.Id,
                    opt => opt.MapFrom(environment => environment.Id))
                .ForMember(environmentDto => environmentDto.Name,
                    opt => opt.MapFrom(environment => environment.Name))
                .ForMember(environmentDto => environmentDto.Text,
                    opt => opt.MapFrom(environment => environment.Text))
                .ForMember(environmentDto => environmentDto.ImageName,
                    opt => opt.MapFrom(environment => environment.ImageName))
                .ForMember(environmentDto => environmentDto.Category,
                    opt => opt.MapFrom(environment => environment.Category.Name))
                .ForMember(environmentDto => environmentDto.ImageSource,
                    opt => opt.MapFrom(environment => environment.ImageSource));
        }
    }
}

using AutoMapper;
using Witcher.Application.Common.Mappings;

namespace Witcher.Application.Environments.Queries.GetEnvironmentDetails
{
    public class EnvironmentVm : IMapWith<Domain.Environment>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Text { get; set; }
        public string? Alias { get; set; }
        public string? Race { get; set; }
        public string? Profession { get; set; }
        public string? Nationality { get; set; }
        public string? ImageName { get; set; }
        public string? ImageSource { get; set; }
        public string Category { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Environment, EnvironmentVm>()
                .ForMember(environmentVm => environmentVm.Id,
                    opt => opt.MapFrom(environment => environment.Id))
                .ForMember(environmentVm => environmentVm.Name,
                    opt => opt.MapFrom(environment => environment.Name))
                .ForMember(environmentVm => environmentVm.Text,
                    opt => opt.MapFrom(environment => environment.Text))
                .ForMember(environmentVm => environmentVm.Alias,
                    opt => opt.MapFrom(environment => environment.Alias))
                .ForMember(environmentVm => environmentVm.Race,
                    opt => opt.MapFrom(environment => environment.Race))
                .ForMember(environmentVm => environmentVm.Profession,
                    opt => opt.MapFrom(environment => environment.Profession))
                .ForMember(environmentVm => environmentVm.Nationality,
                    opt => opt.MapFrom(environment => environment.Nationality))
                .ForMember(environmentVm => environmentVm.ImageName,
                    opt => opt.MapFrom(environment => environment.ImageName))
                .ForMember(environmentVm => environmentVm.Category,
                    opt => opt.MapFrom(environment => environment.Category.Name))
                .ForMember(environmentVm => environmentVm.ImageSource,
                    opt => opt.MapFrom(environment => environment.ImageSource));
        }
    }
}

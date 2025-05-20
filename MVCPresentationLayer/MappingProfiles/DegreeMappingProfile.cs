using DomainLayer.DTO.Degree;
using AutoMapper;
using DomainLayer.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCPresentationLayer.MappingProfiles
{
    internal class DegreeMappingProfile: Profile
    {
        public DegreeMappingProfile() {
            CreateMap<DataLayer.Entity.Degree, DomainLayer.Entity.Degree>().ReverseMap();
            CreateMap<List<DomainLayer.Entity.Degree>, GetDegreesResponseDto>()
            .ForMember(dest => dest.Degrees, src => src.MapFrom(src => src));
            

            CreateMap<Degree, SelectListItem>()
             .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.DegreeTitle))
             .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id.ToString()))
             .ForMember(dest => dest.Selected, opt => opt.Ignore())
             .ForMember(dest => dest.Disabled, opt => opt.Ignore())
             .ForMember(dest => dest.Group, opt => opt.Ignore())
             ;
        }
    }
}

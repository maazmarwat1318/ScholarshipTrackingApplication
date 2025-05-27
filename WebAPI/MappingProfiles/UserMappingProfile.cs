using DomainLayer.DTO.Student;
using AutoMapper;
using DataLayer.Entity;
using DomainLayer.Enums;
using WebAPI.ViewModels.ScholarshipModerator;
using WebAPI.ViewModels.Student;
using DomainLayer.DTO.ScholarshipModerator;

namespace WebAPI.MappingProfiles
{
    internal class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {

            CreateMap<DataLayer.Entity.User, DomainLayer.Entity.User>().ReverseMap();

            CreateMap<DomainLayer.Entity.ScholarshipModerator, DataLayer.Entity.User>()
                .ForMember(dest => dest.ScholarshipModerator, opt => opt.Ignore())
                .ForMember(dest => dest.Student, opt => opt.Ignore());

            CreateMap<DomainLayer.Entity.Student, DataLayer.Entity.User>().ForMember(dest => dest.ScholarshipModerator, opt => opt.Ignore())
                .ForMember(dest => dest.Student, opt => opt.Ignore());

        }
    }
}

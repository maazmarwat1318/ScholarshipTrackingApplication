using AutoMapper;
using WebAPI.ViewModels.Account;
using DomainLayer.DTO.Authentication;

namespace WebAPI.MappingProfiles
{
    internal class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {

            CreateMap<DataLayer.Entity.User, DomainLayer.Entity.User>().ReverseMap();

            CreateMap<LogInViewModel, LogInRequest>();
            CreateMap<ForgotPasswordViewModel, ForgotPasswordRequest>();
            CreateMap<ResetPasswordViewModel, ResetPasswordRequest>().ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.NewPassword));

            CreateMap<DomainLayer.Entity.ScholarshipModerator, DataLayer.Entity.User>()
                .ForMember(dest => dest.ScholarshipModerator, opt => opt.Ignore())
                .ForMember(dest => dest.Student, opt => opt.Ignore());

            CreateMap<DomainLayer.Entity.Student, DataLayer.Entity.User>().ForMember(dest => dest.ScholarshipModerator, opt => opt.Ignore())
                .ForMember(dest => dest.Student, opt => opt.Ignore());

        }
    }
}

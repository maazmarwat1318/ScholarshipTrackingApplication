using AutoMapper;
using WebAPI.ViewModels.Account;
using DomainLayer.DTO.Authentication;

namespace WebAPI.MappingProfiles
{
    internal class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {

            CreateMap<ForgotPasswordViewModel, ForgotPasswordRequest>();

            CreateMap<ResetPasswordViewModel, ResetPasswordRequest>().ForMember(s => s.Password, opt => opt.MapFrom(dest => dest.NewPassword));

            CreateMap<LogInViewModel, LogInRequest>();


        }
    }
}

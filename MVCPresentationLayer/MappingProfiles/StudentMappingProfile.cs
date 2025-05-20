using DomainLayer.DTO.Student;
using AutoMapper;
using DataLayer.Entity;
using DomainLayer.Enums;
using MVCPresentationLayer.ViewModels.ScholarshipModerator;
using MVCPresentationLayer.ViewModels.Student;
using DomainLayer.DTO.ScholarshipModerator;

namespace MVCPresentationLayer.MappingProfiles
{
    internal class StudentMappingProfile : Profile
    {
        public StudentMappingProfile()
        {
            CreateMap<DataLayer.Entity.User, DomainLayer.Entity.User>().ReverseMap();
            CreateMap<DomainLayer.Entity.ScholarshipModerator, DataLayer.Entity.User>()
                .ForMember(dest => dest.ScholarshipModerator, opt => opt.Ignore())
             .ForMember(dest => dest.Student, opt => opt.Ignore());

            CreateMap<DomainLayer.Entity.Student, DomainLayer.DTO.Student.StudentResponse>();

            CreateMap<CreateStudentViewModel, DomainLayer.Entity.Student>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => "00000000"))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Role.Student));

            CreateMap<CreateScholarshipModeratorViewModel, DomainLayer.Entity.ScholarshipModerator>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => "00000000"));


            CreateMap<DomainLayer.Entity.Student, DataLayer.Entity.Student>()
            .ForMember(dest => dest.Applications, opt => opt.Ignore())
            .ForMember(dest => dest.StudentNavigation, opt => opt.Ignore())
            .ForMember(dest => dest.Degree, opt => opt.Ignore());

            CreateMap<GetStudentsViewModel, GetStudentsRequest>()
            .ForMember(dest => dest.PageSize, opt => opt.Ignore());

            CreateMap<GetStudentsResponse, GetStudentsViewModel>();

            CreateMap<GetModeratorsViewModel, GetModeratorsRequest>()
            .ForMember(dest => dest.PageSize, opt => opt.Ignore());

            CreateMap<GetModeratorsResponse, GetModeratorsViewModel>();



            CreateMap<DomainLayer.Entity.ScholarshipModerator, DataLayer.Entity.ScholarshipModerator>()
            .ForMember(dest => dest.Offerings, opt => opt.Ignore())
            .ForMember(dest => dest.Moderator, opt => opt.Ignore())
            .ForMember(dest => dest.ApplicationStatusHistories, opt => opt.Ignore());


            CreateMap<DomainLayer.Entity.Student, DataLayer.Entity.User>().ForMember(dest => dest.ScholarshipModerator, opt => opt.Ignore())
             .ForMember(dest => dest.Student, opt => opt.Ignore());

            CreateMap<Tuple<DataLayer.Entity.Student, DataLayer.Entity.User>, StudentResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Item1.Id))
                .ForMember(dest => dest.DegreeId, opt => opt.MapFrom(src => src.Item1.DegreeId))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Item2.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Item2.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Item2.LastName))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Item2.Role));

            CreateMap<Tuple<DataLayer.Entity.ScholarshipModerator, DataLayer.Entity.User>, ScholarshipModeratorResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Item1.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Item2.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Item2.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Item2.LastName))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Item2.Role));

        }
    }
}

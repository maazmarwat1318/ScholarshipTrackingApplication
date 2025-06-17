using DomainLayer.DTO.Student;
using AutoMapper;
using DomainLayer.Enums;
using MVCPresentationLayer.ViewModels.Student;

namespace MVCPresentationLayer.MappingProfiles
{
    internal class StudentMappingProfile : Profile
    {
        public StudentMappingProfile()
        {

            CreateMap<DomainLayer.Entity.Student, DomainLayer.DTO.Student.StudentResponse>();

            CreateMap<EditStudentViewModel, EditStudentRequest>().ReverseMap().ForMember(s => s.Degrees, opt => opt.Ignore());

            CreateMap<StudentResponse, EditStudentViewModel>().ForMember(s => s.Degrees, opt => opt.Ignore()).ForMember(s => s.StudentId, opt => opt.MapFrom(dest => dest.Id));

            CreateMap<CreateStudentViewModel, DomainLayer.Entity.Student>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => "00000000"))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Role.Student));

            CreateMap<DomainLayer.Entity.Student, DataLayer.Entity.Student>()
                .ForMember(dest => dest.Applications, opt => opt.Ignore())
                .ForMember(dest => dest.StudentNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.Degree, opt => opt.Ignore());

            CreateMap<GetStudentsViewModel, GetStudentsRequest>()
                .ForMember(dest => dest.PageSize, opt => opt.Ignore());

            CreateMap<GetStudentsViewModel, SearchStudentsViaNameRequest>()
                .ForMember(dest => dest.PageSize, opt => opt.Ignore());

            CreateMap<GetStudentsResponse, GetStudentsViewModel>()
                .ForMember(dest => dest.PageSize, opt => opt.Ignore());

            CreateMap<Tuple<DataLayer.Entity.Student, DataLayer.Entity.User, DataLayer.Entity.Degree?>, StudentResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Item1.Id))
                .ForMember(dest => dest.DegreeId, opt => opt.MapFrom(src => src.Item1.DegreeId))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Item2.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Item2.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Item2.LastName))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Item2.Role));

            CreateMap<Tuple<DataLayer.Entity.Student, DataLayer.Entity.User, DataLayer.Entity.Degree?>, StudentResponseWithDegree>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Item1.Id))
                .ForMember(dest => dest.DegreeTitle, opt => opt.MapFrom(src => src.Item3 == null ? null : src.Item3.DegreeTitle))
                .ForMember(dest => dest.DegreeId, opt => opt.MapFrom(src => src.Item1.DegreeId))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Item2.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Item2.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Item2.LastName))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Item2.Role));

        }
    }
}

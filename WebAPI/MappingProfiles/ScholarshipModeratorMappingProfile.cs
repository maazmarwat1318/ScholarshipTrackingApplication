﻿using AutoMapper;
using WebAPI.ViewModels.ScholarshipModerator;
using DomainLayer.DTO.ScholarshipModerator;

namespace WebAPI.MappingProfiles
{
    internal class ScholarshipModeratorMappingProfile : Profile
    {
        public ScholarshipModeratorMappingProfile()
        {
            CreateMap<CreateScholarshipModeratorViewModel, DomainLayer.Entity.ScholarshipModerator>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => "00000000"));

            CreateMap<EditScholarshipModeratorViewModel, EditScholarshipModeratorRequest>().ReverseMap();

            CreateMap<ScholarshipModeratorResponse, EditScholarshipModeratorViewModel>().ForMember(s => s.ModeratorId, opt => opt.MapFrom(dest => dest.Id));

            CreateMap<GetModeratorsViewModel, GetModeratorsRequest>()
                .ForMember(dest => dest.PageSize, opt => opt.Ignore());

            CreateMap<GetModeratorsResponse, GetModeratorsViewModel>()
                .ForMember(dest => dest.PageSize, opt => opt.Ignore());

            CreateMap<GetModeratorsViewModel, SearchModeratorViaNameRequest>()
                .ForMember(dest => dest.PageSize, opt => opt.Ignore());


            CreateMap<DomainLayer.Entity.ScholarshipModerator, DataLayer.Entity.ScholarshipModerator>()
                .ForMember(dest => dest.Offerings, opt => opt.Ignore())
                .ForMember(dest => dest.Moderator, opt => opt.Ignore())
                .ForMember(dest => dest.ApplicationStatusHistories, opt => opt.Ignore());

            CreateMap<Tuple<DataLayer.Entity.ScholarshipModerator, DataLayer.Entity.User>, ScholarshipModeratorResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Item1.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Item2.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Item2.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Item2.LastName))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Item2.Role));

        }
    }
}

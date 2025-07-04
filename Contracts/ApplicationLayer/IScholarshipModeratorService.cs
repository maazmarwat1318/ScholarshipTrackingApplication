﻿using DomainLayer.Common;
using DomainLayer.DTO.Common;
using DomainLayer.DTO.ScholarshipModerator;
using DomainLayer.Entity;

namespace Contracts.ApplicationLayer.Interface
{
    public interface IScholarshipModeratorService
    {
        Task<Response<MessageResponse>> AddScholarshipModerator(ScholarshipModerator student);

        Task<Response<GetModeratorsResponse>> GetModerators(GetModeratorsRequest request);

        Task<Response<GetModeratorsResponse>> SearchModeratorsViaName(SearchModeratorViaNameRequest request);

        Task<Response<MessageResponse>> EditModerator(EditScholarshipModeratorRequest request);

        Task<Response<ScholarshipModeratorResponse>> GetModeratorById(int id);
    }
}

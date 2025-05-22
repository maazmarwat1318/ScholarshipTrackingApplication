using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Common;
using DomainLayer.DTO.Authentication;
using DomainLayer.DTO.Common;
using DomainLayer.DTO.ScholarshipModerator;
using DomainLayer.DTO.Student;
using DomainLayer.Entity;

namespace Contracts.ApplicationLayer.Interface
{
    public interface IScholarshipModeratorService
    {
        Task<Response<MessageResponse>> AddScholarshipModerator(ScholarshipModerator student);

        Task<GetModeratorsResponse> GetModerators(GetModeratorsRequest request);

        Task<GetModeratorsResponse> SearchModeratorsViaName(SearchModeratorViaNameRequest request);
    }
}

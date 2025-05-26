using DomainLayer.Common;
using DomainLayer.DTO.ScholarshipModerator;
using DomainLayer.Entity;

namespace Contracts.DataLayer
{
    public interface IScholarshipModeratorRepository
    {
        Task<Response<bool>> AddScholarshipModerator(ScholarshipModerator moderator);
        Task<GetModeratorsResponse> GetModerators(GetModeratorsRequest request);

        Task<GetModeratorsResponse> SearchModeratosViaName(SearchModeratorViaNameRequest request);

        Task<Response<bool>> EditModerator(EditScholarshipModeratorRequest request);

        Task<ScholarshipModeratorResponse?> GetModeratorById(int id);
    }
}

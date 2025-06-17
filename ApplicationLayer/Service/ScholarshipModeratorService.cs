using Contracts.ApplicationLayer.Interface;
using Contracts.DataLayer;
using DomainLayer.Common;
using DomainLayer.DTO.Common;
using DomainLayer.DTO.ScholarshipModerator;
using DomainLayer.Entity;
using DomainLayer.Errors.AuthenticationErrors;

namespace ApplicationLayer.Service
{
    public class ScholarshipModeratorService : IScholarshipModeratorService
    {

        private readonly IScholarshipModeratorRepository _scholarshipModeratorRepo;


        public ScholarshipModeratorService(IScholarshipModeratorRepository scholarshipModeratorRepo)
        {

            _scholarshipModeratorRepo = scholarshipModeratorRepo;
        }


        public async Task<Response<MessageResponse>> AddScholarshipModerator(ScholarshipModerator moderator)
        {
            var result = await _scholarshipModeratorRepo.AddScholarshipModerator(moderator);
            if (!result.IsSuccess)
            {
                return Response<MessageResponse>.Failure(result.ServiceError!);
            }
            return Response<MessageResponse>.Success(new()
            {
                Message = "Moderator created successfully"
            });
        }

        public async Task<Response<MessageResponse>> EditModerator(EditScholarshipModeratorRequest request)
        {
            var result = await _scholarshipModeratorRepo.EditModerator(request);
            return Response<MessageResponse>.Success(new()
            {
                Message = "Moderator Updated Successfuly"
            });
        }

        public async Task<Response<GetModeratorsResponse>> GetModerators(GetModeratorsRequest request)
        {
            var result = await _scholarshipModeratorRepo.GetModerators(request);
            return Response<GetModeratorsResponse>.Success(result);

        }
        public async Task<Response<GetModeratorsResponse>> SearchModeratorsViaName(SearchModeratorViaNameRequest request)
        {
            var result = await _scholarshipModeratorRepo.SearchModeratosViaName(request);
            return Response<GetModeratorsResponse>.Success(result);
        }

        public async Task<Response<ScholarshipModeratorResponse>> GetModeratorById(int id)
        {
            var moderator = await _scholarshipModeratorRepo.GetModeratorById(id);
            if (moderator == null)
            {
                return Response<ScholarshipModeratorResponse>.Failure(AccountErrorHelper.UserNotFoundError());
            }
            return Response<ScholarshipModeratorResponse>.Success(moderator);
        }
    }
}

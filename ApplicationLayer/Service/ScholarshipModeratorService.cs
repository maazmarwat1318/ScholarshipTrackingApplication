using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.ApplicationLayer.Interface;
using Contracts.DataLayer;
using Contracts.InfrastructureLayer;
using DomainLayer.Common;
using DomainLayer.DTO.Common;
using DomainLayer.DTO.ScholarshipModerator;
using DomainLayer.DTO.Student;
using DomainLayer.Entity;

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

        public async Task<GetModeratorsResponse> GetModerators(GetModeratorsRequest request)
        {
            var result = await _scholarshipModeratorRepo.GetModerators(request);
            return result;

        }
    }
}

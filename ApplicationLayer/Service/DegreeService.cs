using DomainLayer.Common;
using Contracts.ApplicationLayer.Interface;
using Contracts.DataLayer;
using Contracts.InfrastructureLayer;
using DomainLayer.Entity;

namespace ApplicationLayer.Service
{
    public class DegreeService : IDegreeService
    {
        private readonly IDegreeRepository _degreeRepo;

        public DegreeService(IJwtService jwtService, IDegreeRepository degreeRepo, ICrypterService crypterService, IEmailService emailService, ICaptchaVerificationService captchaService)
        {
            _degreeRepo = degreeRepo;
        }

        public async Task<Response<List<Degree>>> GetAllDegrees()
        {
            var degress = await _degreeRepo.GetAllDegrees();
            return Response<List<Degree>>.Success(degress);
        }
    }
}

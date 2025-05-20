using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Common;
using DomainLayer.DTO.Authentication;
using Contracts.ApplicationLayer.Interface;
using Contracts.DataLayer;
using Contracts.InfrastructureLayer;
using DomainLayer.Entity;
using DomainLayer.Errors.AuthenticationErrors;
using DomainLayer.DTO.Common;
using System.Security.Claims;
using DomainLayer.DTO.Degree;

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

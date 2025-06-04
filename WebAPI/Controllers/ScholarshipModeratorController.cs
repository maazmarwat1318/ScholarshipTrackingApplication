using Contracts.ApplicationLayer.Interface;
using AutoMapper;
using DomainLayer.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DomainLayer.DTO.ScholarshipModerator;
using System.Threading.Tasks;
using ApplicationLayer.Service;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAPI.Controllers;
using WebAPI.Extensions;
using WebAPI.ViewModels.ScholarshipModerator;
using DomainLayer.Errors;
using DomainLayer.DTO.Student;
using WebAPI.ViewModels.Student;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScholarshipModeratorController : ControllerBase
    {
        private readonly IScholarshipModeratorService _scholarshipModeratorService;
        private readonly IAccountService _accountService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ScholarshipModeratorController(IScholarshipModeratorService scholarshipModeratorService, ILogger<AccountController> logger, IMapper mapper, IAccountService accountService)
        {
            _accountService = accountService;
            _scholarshipModeratorService = scholarshipModeratorService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "SuperModerator, Moderator")]
        public async Task<IActionResult> Index([FromQuery] GetModeratorsViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return this.BadRequestErrorResponse();
                }

                var response = model.SearchString == null ? await _scholarshipModeratorService.GetModerators(_mapper.Map<GetModeratorsRequest>(model)) : await _scholarshipModeratorService.SearchModeratorsViaName(_mapper.Map<SearchModeratorViaNameRequest>(model));
                if (!response.IsSuccess)
                {
                    return this.ErrorToHttpResponse(response.ServiceError!);
                }
                return this.SuccessObjectToHttpResponse(response.Value!);
            }
            catch (Exception ex)
            {
                return OnUnknowException(ex, nameof(Create));
            }

        }

        [HttpGet("{id}")]
        [Authorize(Roles = "SuperModerator, Moderator")]
        public async Task<IActionResult> Index([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return this.BadRequestErrorResponse();
                }

                var response = await _scholarshipModeratorService.GetModeratorById(id);
                if (!response.IsSuccess)
                {
                    return this.ErrorToHttpResponse(response.ServiceError!);
                }
                var val = this.SuccessObjectToHttpResponse(response.Value!);
                return val;
            }
            catch (Exception ex)
            {
                return OnUnknowException(ex, nameof(Create));
            }

        }

        [HttpPost("[action]")]
        [Authorize(Roles = "SuperModerator")]
        public async Task<IActionResult> Edit(EditScholarshipModeratorRequest request)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return this.BadRequestErrorResponse();
                }

                var response = await _scholarshipModeratorService.EditModerator(_mapper.Map<EditScholarshipModeratorRequest>(request));

                if(!response.IsSuccess)
                {
                    return this.ErrorToHttpResponse(response.ServiceError!);
                }

                return this.SuccessObjectToHttpResponse(response.Value!);

            }
            catch (Exception ex)
            {
                return OnUnknowException(ex, nameof(Edit));
            }

        }

        [HttpPost("[action]")]
        [Authorize(Roles = "SuperModerator")]
        public async Task<IActionResult> Create(CreateScholarshipModeratorViewModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return this.BadRequestErrorResponse();
                }

                var response = await _scholarshipModeratorService.AddScholarshipModerator(_mapper.Map<ScholarshipModerator>(request));
                if (!response.IsSuccess)
                {
                    return this.ErrorToHttpResponse(response.ServiceError!);
                }

                return this.SuccessObjectToHttpResponse(response.Value!);

            }
            catch (Exception ex)
            {
                return OnUnknowException(ex, nameof(Create));
            }

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperModerator")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return this.BadRequestErrorResponse();
                }

                var response = await _accountService.DeleteUser(id);
                
                if(!response.IsSuccess)
                {
                    return this.ErrorToHttpResponse(response.ServiceError!);
                }

                return this.SuccessObjectToHttpResponse(response.Value!);
            }
            catch (Exception ex)
            {
                return OnUnknowException(ex, nameof(Delete));
            }

        }

        private IActionResult OnUnknowException(Exception ex, string action)
        {
            _logger.LogError(ex, $"Unknown error occured at {nameof(ScholarshipModeratorController)} in action {action}");
            return this.ErrorToHttpResponse(CommonErrorHelper.ServerError());
        }
    }
}

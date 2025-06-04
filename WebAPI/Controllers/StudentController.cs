using Contracts.ApplicationLayer.Interface;
using AutoMapper;
using DomainLayer.Entity;
using DomainLayer.Enums;
using InfrastructureLayer.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using DomainLayer.DTO.Student;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebAPI.ViewModels.Student;
using WebAPI.Extensions;
using DomainLayer.Errors;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IAccountService _accountService;
        private readonly IDegreeService _degreeService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public StudentController(IStudentService studentService, IAccountService accountService, IDegreeService degreeService, ILogger<StudentController> logger, IMapper mapper)
        {
            _studentService = studentService;
            _degreeService = degreeService;
            _logger = logger;
            _mapper = mapper;
            _accountService = accountService;
        }

        [HttpGet]
        [Authorize(Roles = "SuperModerator, Moderator")]
        public async Task<IActionResult> Index([FromQuery] GetStudentsViewModel model)
        {
            throw new Exception();
            try
            {
                if (!ModelState.IsValid)
                {
                    return this.BadRequestErrorResponse();
                }

                var response = model.SearchString == null ? await _studentService.GetStudents(_mapper.Map<GetStudentsRequest>(model)) : await _studentService.SearchStudentViaName(_mapper.Map<SearchStudentsViaNameRequest>(model));
                if(!response.IsSuccess)
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

                var response = await _studentService.GetStudentById(id);
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

        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperModerator, Moderator")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return this.BadRequestErrorResponse();
                }

                var response = await _accountService.DeleteUser(id);

                if (!response.IsSuccess)
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

        [HttpPost("[action]")]
        [Authorize(Roles = "SuperModerator, Moderator")]
        public async Task<IActionResult> Create(CreateStudentViewModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return this.BadRequestErrorResponse();
                }

                var response = await _studentService.AddStudent(_mapper.Map<Student>(request));
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

        [HttpPost("[action]")]
        [Authorize(Roles = "SuperModerator, Moderator")]
        public async Task<IActionResult> Edit(EditStudentViewModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return this.BadRequestErrorResponse();
                }

                var response = await _studentService.EditStudent(_mapper.Map<EditStudentRequest>(request));
                if (!response.IsSuccess)
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

        private IActionResult OnUnknowException(Exception ex, string action)
        {
            _logger.LogError(ex, $"Unknown error occured at {nameof(StudentController)} in action {action}");
            return this.ErrorToHttpResponse(CommonErrorHelper.ServerError());

        }
    }
}

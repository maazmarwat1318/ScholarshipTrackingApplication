using Contracts.ApplicationLayer.Interface;
using AutoMapper;
using DomainLayer.Entity;
using DomainLayer.Enums;
using InfrastructureLayer.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using MVCPresentationLayer.Extensions;
using MVCPresentationLayer.Helpers;
using MVCPresentationLayer.ViewModels.ScholarshipModerator;
using ApplicationLayer.Service;
using DomainLayer.DTO.Student;
using MVCPresentationLayer.ViewModels.Student;
using DomainLayer.DTO.ScholarshipModerator;

namespace MVCPresentationLayer.Controllers
{
    public class ScholarshipModeratorController : ControllerWithHelpers
    {
        private readonly IScholarshipModeratorService _scholarshipModeratorService;
        private readonly IDegreeService _degreeService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ScholarshipModeratorController(IScholarshipModeratorService scholarshipModeratorService, IDegreeService degreeService, ILogger<AccountController> logger, IMapper mapper)
        {
            _scholarshipModeratorService = scholarshipModeratorService;
            _degreeService = degreeService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(GetModeratorsViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.Page = 1;
                }
                var request = _mapper.Map<GetModeratorsRequest>(model);
                var response = await _scholarshipModeratorService.GetModerators(request);
                model = _mapper.Map<GetModeratorsViewModel>(response);
                return View(model);

            }
            catch (Exception ex)
            {
                return OnUnknowException(ex, nameof(Create));
            }

        }

        [HttpGet]
        [Authorize(Roles = "SuperModerator")]
        public async Task<IActionResult> Create(CreateScholarshipModeratorViewModel? request)
        {
            try
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
                var degrees = await _degreeService.GetAllDegrees();
                var viewModel = request ?? new CreateScholarshipModeratorViewModel();
                return View(viewModel);

            }
            catch (Exception ex)
            {
                return OnUnknowException(ex, nameof(Create));
            }
            
        }

        [HttpPost]
        [Authorize(Roles = "SuperModerator")]
        public async Task<IActionResult> CreateScholarshipModerator(CreateScholarshipModeratorViewModel request)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = ModelState.GetFirstErrorMessage();
                    return RedirectToAction("Create", request);
                }

                var result = await _scholarshipModeratorService.AddScholarshipModerator(_mapper.Map<ScholarshipModerator>(request));
                if(!result.IsSuccess)
                {
                    TempData["ErrorMessage"] = result.ServiceError!.Message;
                    return RedirectToAction("Create", request);
                }

                TempData["SuccessMessage"] = result.Value!.Message;
                return RedirectToAction("Create");
                
            }
            catch (Exception ex)
            {
                return OnUnknowException(ex, nameof(CreateScholarshipModerator));
            }

        }

        private IActionResult OnUnknowException(Exception ex, string action)
        {
            _logger.LogError(ex, $"Unknown error occured at ${nameof(ScholarshipModeratorController)} in action ${action}");
            return GetUnknownErrorView();
        }
    }
}

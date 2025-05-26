using Contracts.ApplicationLayer.Interface;
using AutoMapper;
using DomainLayer.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCPresentationLayer.Extensions;
using MVCPresentationLayer.Helpers;
using MVCPresentationLayer.ViewModels.ScholarshipModerator;
using DomainLayer.DTO.ScholarshipModerator;
using System.Threading.Tasks;
using ApplicationLayer.Service;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCPresentationLayer.ViewModels.Student;

namespace MVCPresentationLayer.Controllers
{
    public class ScholarshipModeratorController : ControllerWithHelpers
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
        [Authorize(Roles = "SuperModerator")]

        public async Task<IActionResult> Index(GetModeratorsViewModel model)
        {
            try
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
                if (!ModelState.IsValid)
                {
                    model.Page = 1;
                }
                var response = model.SearchString == "" ? await _scholarshipModeratorService.GetModerators(_mapper.Map<GetModeratorsRequest>(model)) : await _scholarshipModeratorService.SearchModeratorsViaName(_mapper.Map<SearchModeratorViaNameRequest>(model));
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
        public IActionResult Create(CreateScholarshipModeratorViewModel? request)
        {
            try
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
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
        public async Task<IActionResult> EditModerator(EditScholarshipModeratorViewModel request)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = ModelState.GetFirstErrorMessage();
                    return RedirectToAction("Edit", new { id = request.ModeratorId });
                }

                var result = await _scholarshipModeratorService.EditModerator(_mapper.Map<EditScholarshipModeratorRequest>(request));

                TempData["SuccessMessage"] = result.Value!.Message;
                return RedirectToAction("Edit", new { id = request.ModeratorId });

            }
            catch (Exception ex)
            {
                return OnUnknowException(ex, nameof(EditModerator));
            }

        }

        [HttpGet]
        [Authorize(Roles = "SuperModerator")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var moderator = await _scholarshipModeratorService.GetModeratorById(id);
                if (!moderator.IsSuccess)
                {
                    TempData["ErrorMessage"] = moderator.ServiceError!.Message;
                    return RedirectToAction("Index");
                }
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
                var viewModel = _mapper.Map<EditScholarshipModeratorViewModel>(moderator.Value);
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
                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = ModelState.GetFirstErrorMessage();
                    return RedirectToAction("Create", request);
                }

                var result = await _scholarshipModeratorService.AddScholarshipModerator(_mapper.Map<ScholarshipModerator>(request));
                if (!result.IsSuccess)
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

        [HttpPost]
        [Authorize(Roles = "SuperModerator")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = ModelState.GetFirstErrorMessage();
                    return RedirectToAction("Index");
                }

                await _accountService.DeleteUser(id);
                TempData["SuccessMessage"] = "Moderator Deleted Successfuly";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                return OnUnknowException(ex, nameof(Create));
            }

        }

        private IActionResult OnUnknowException(Exception ex, string action)
        {
            _logger.LogError(ex, $"Unknown error occured at ${nameof(ScholarshipModeratorController)} in action ${action}");
            return GetUnknownErrorView();
        }
    }
}

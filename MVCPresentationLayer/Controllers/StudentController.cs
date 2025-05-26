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
using MVCPresentationLayer.ViewModels.Student;
using DomainLayer.DTO.Student;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MVCPresentationLayer.Controllers
{
    public class StudentController : ControllerWithHelpers
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
        public async Task<IActionResult> Index(GetStudentsViewModel model)
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            try
            {
                if (!ModelState.IsValid)
                {
                    model.Page = 1;
                }
                

                var response = model.SearchString == "" ? await _studentService.GetStudents(_mapper.Map<GetStudentsRequest>(model)) : await _studentService.SearchStudentViaName(_mapper.Map<SearchStudentsViaNameRequest>(model));
                model = _mapper.Map<GetStudentsViewModel>(response);
                return View(model);
            }
            catch (Exception ex)
            {
                return OnUnknowException(ex, nameof(Create));
            }

        }

        [HttpPost]
        [Authorize(Roles = "SuperModerator, Moderator")]
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
                TempData["SuccessMessage"] = "Student Deleted Successfuly";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                return OnUnknowException(ex, nameof(Create));
            }

        }

        [HttpGet]
        [Authorize(Roles = "SuperModerator, Moderator")]
        public async Task<IActionResult> Create(CreateStudentViewModel? request)
        {
            try
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
                var degrees = await _degreeService.GetAllDegrees();
                var viewModel = request ?? new CreateStudentViewModel();
                viewModel.Degrees = degrees.Value!.Select(deg => _mapper.Map<SelectListItem>(deg)).ToList();
                return View(viewModel);

            }
            catch (Exception ex)
            {
                return OnUnknowException(ex, nameof(Create));
            }

        }

        [HttpPost]
        [Authorize(Roles = "SuperModerator, Moderator")]
        public async Task<IActionResult> CreateStudent(CreateStudentViewModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = ModelState.GetFirstErrorMessage();
                    return RedirectToAction("Create", request);
                }

                var result = await _studentService.AddStudent(_mapper.Map<Student>(request));
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
                return OnUnknowException(ex, nameof(CreateStudent));
            }

        }

        [HttpGet]
        [Authorize(Roles = "SuperModerator, Moderator")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var student = await _studentService.GetStudentById(id);
                if(!student.IsSuccess)
                {
                    TempData["ErrorMessage"] = student.ServiceError!.Message;
                    return RedirectToAction("Index");
                }
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
                var degrees = await _degreeService.GetAllDegrees();
                var viewModel = _mapper.Map<EditStudentViewModel>(student.Value);
                viewModel.Degrees = [.. degrees.Value!.Select(deg => _mapper.Map<SelectListItem>(deg))];
                return View(viewModel);
            }
            catch (Exception ex)
            {
                return OnUnknowException(ex, nameof(Create));
            }

        }

        [HttpPost]
        [Authorize(Roles = "SuperModerator, Moderator")]
        public async Task<IActionResult> EditStudent(EditStudentViewModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = ModelState.GetFirstErrorMessage();
                    return RedirectToAction("Edit", new { id = request.StudentId });
                }

                var result = await _studentService.EditStudent(_mapper.Map<EditStudentRequest>(request));

                TempData["SuccessMessage"] = result.Value!.Message;
                return RedirectToAction("Edit", new { id = request.StudentId });

            }
            catch (Exception ex)
            {
                return OnUnknowException(ex, nameof(EditStudent));
            }

        }

        private IActionResult OnUnknowException(Exception ex, string action)
        {
            _logger.LogError(ex, $"Unknown error occured at ${nameof(StudentController)} in action ${action}");
            return GetUnknownErrorView();
        }
    }
}

﻿using Contracts.ApplicationLayer.Interface;
using DomainLayer.Errors;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DegreeController : ControllerBase
    {
        private readonly IDegreeService _degreeService;
        private readonly ILogger _logger;

        public DegreeController(IDegreeService degreeService, ILogger<AccountController> logger)
        {
            _degreeService = degreeService;
            _logger = logger;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetDegrees()
        {
            try
            {

                var response = await _degreeService.GetAllDegrees();
                if (!response.IsSuccess)
                {
                    return this.ErrorToHttpResponse(response.ServiceError!);
                }
                ;

                return this.SuccessObjectToHttpResponse(response.Value!);
            }
            catch (Exception ex)
            {
                return OnUnknowException(ex, nameof(GetDegrees));
            }
        }


        private IActionResult OnUnknowException(Exception ex, string action)
        {
            _logger.LogError(ex, $"Unknown error occured at {nameof(DegreeController)} in action {action}");
            return this.ErrorToHttpResponse(CommonErrorHelper.ServerError());
        }

    }
}
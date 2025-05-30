using DomainLayer.Errors;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static IActionResult ErrorToHttpResponse(this ControllerBase controller, ServiceError error)
        {
            return controller.StatusCode(error.StatusCode, error.ToHttpResponse());
        }

        public static IActionResult BadRequestErrorResponse(this ControllerBase controller)
        {
            return controller.BadRequest(CommonErrorHelper.BadRequestError(controller.ModelState.GetFirstErrorMessage()).ToHttpResponse());
        }

        public static IActionResult SuccessObjectToHttpResponse(this ControllerBase controller, object successResponse)
        {
            return controller.Ok(successResponse);
        }
    }
}

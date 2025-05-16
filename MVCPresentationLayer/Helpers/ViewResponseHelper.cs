using Microsoft.AspNetCore.Mvc;
using MVCPresentationLayer.ViewModels;


namespace MVCPresentationLayer.Helpers
{
    public class ControllerWithHelpers: Controller
    {
        protected IActionResult GetErrorView(string message, string title)
        {
            return View("Error", new ErrorViewModel { ErrorMessage = message, ErrorTitle= title });
        }

        protected IActionResult GetUnknownErrorView()
        {
            return View("Error", new ErrorViewModel { ErrorMessage = "Oops! Unknow Server Error Occured", ErrorTitle = "Unknown Server Error" });
        }
    }
}

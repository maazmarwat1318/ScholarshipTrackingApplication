using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCPresentationLayer.ViewModels;

namespace MVCPresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var authCookie = Request.Cookies["jwt"];
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }


    }
}

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Rompecabezas.Web.Controllers
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
            ViewBag.ErrorPin = TempData["ErrorPin"];
            return View();
        }

    }
}
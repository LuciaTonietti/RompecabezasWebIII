using Microsoft.AspNetCore.Mvc;

namespace Rompecabezas.Web.Controllers
{
    public class SalaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

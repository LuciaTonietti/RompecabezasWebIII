using Microsoft.AspNetCore.Mvc;
using Rompecabezas.Logica.Models;
using Rompecabezas.Logica.Servicios;


namespace Rompecabezas.Web.Controllers
{
    public class SalaController : Controller
    {
        private readonly ISalaService _entityFrameworkService;
        public SalaController(ISalaService entityFrameworkService)
        {
            _entityFrameworkService = entityFrameworkService;
        }

        public IActionResult Crear()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Crear(Sala sala)
        {
            if (ModelState.IsValid)
            {
                if (!_entityFrameworkService.EstaReptidoElNickName(sala.NickName))
                {
                    Sala? guardada = _entityFrameworkService.AgregarSala(sala);
                    if (guardada != null)
                    {
                        return View("Sala", guardada);
                    }
                }
                ViewBag.Message = "El nombre ingresado ya está en uso!";
                ViewBag.Classes = "alert alert-danger text-center row col-lg-4";
                return View();
            }
            return View();
        }
        [HttpPost]
        public IActionResult Ingresar(IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                int nroSala = int.Parse(form["nroSala"]);
                string pinIngresado = form["pin"];
                string nombreUsuario = form["NickName"];
                try
                {
                    Sala sala = _entityFrameworkService.ObtenerSala(nroSala, pinIngresado, nombreUsuario);
                    ViewBag.NombreUsuario = nombreUsuario;
                    return View("Sala", sala);
                }
                catch (Exception ex)
                {
                    TempData["ErrorPin"] = ex.Message;
                } 
            }
            return RedirectToAction("Index", "Home");
        }
    }
}

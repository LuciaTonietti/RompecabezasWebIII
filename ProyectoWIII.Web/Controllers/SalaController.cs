using Microsoft.AspNetCore.Mvc;
using Rompecabezas.Logica.Entidades;

namespace Rompecabezas.Web.Controllers
{
    public class SalaController : Controller
    {
        public IActionResult Crear()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Crear(IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                Sala sala = new Sala();
                sala.NombreUsuarioCreador = form["NombreUsuarioCreador"];
                sala.CantPiezas = int.Parse(form["CantPiezas"]);
                string Pin = form["Pin"];
                sala.Pin = string.IsNullOrEmpty(Pin) ? null : Pin;
                TempData["nroSala"] = 1;//Este valor lo deberia devolver el servicio cuando crea la sala en la base de datos.
                return RedirectToAction("Ingresar");
            }
            return View();
        }
    }
}

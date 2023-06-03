using Microsoft.AspNetCore.Mvc;
using Rompecabezas.Logica.Entidades;
using Rompecabezas.Logica.Servicios;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace Rompecabezas.Web.Controllers
{
    public class SalaController : Controller
    {
        private ISalaService _salaService;
        public SalaController(ISalaService salaService)
        {
            _salaService = salaService;
        }

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
                return View("Sala", sala);
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
                string nombreUsuario = form["nombreUsuario"];
                Sala sala = _salaService.ObtenerSala((int)nroSala);
                if (!string.IsNullOrEmpty(sala.Pin))
                {
                    string resultadoVerificacion = this.VerificarPin(sala.Pin, pinIngresado);
                    if (!string.IsNullOrEmpty(resultadoVerificacion))
                    {
                        TempData["ErrorPin"] = resultadoVerificacion;
                        return RedirectToAction("Index", "Home");
                    }
                }
                return View("Sala", sala);
            }
            return RedirectToAction("Index", "Home");
        }

        private string VerificarPin(string pinSala, string pinIngresado)
        {
            string msnjError = null;
            if (string.IsNullOrEmpty(pinIngresado))
            {
                msnjError = "La sala es privada debe Ingresar un PIN.";
            }
            if (string.IsNullOrEmpty(msnjError) && !pinIngresado.Equals(pinSala))
            {
                msnjError = "EL PIN ingresado no es valido.";
            }
            return msnjError;
        }
    }
}

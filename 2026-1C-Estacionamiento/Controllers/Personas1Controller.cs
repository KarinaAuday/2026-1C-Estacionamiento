using _2026_1C_Estacionamiento.Models;
using Microsoft.AspNetCore.Mvc;

namespace _2026_1C_Estacionamiento.Controllers
{
    public class Personas1Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult CrearPersona(string nombre, string apellido)
        {
            Persona persona = new Persona
            {
                Nombre = nombre,
                Apellido = apellido
            };
            return View("MostrarDatos", persona);
        }

        public IActionResult CrearGet()
        {
            return View();
        }

        public IActionResult CrearPost()
        {
            return View();
        }

        public IActionResult CrearPersona2(string nombre, string apellido)
        {
            // Aquí podrías guardar la persona en una base de datos o realizar alguna acción con los datos recibidos.
            // Por ahora, simplemente los mostramos en la vista.
            ViewBag.Nombre = nombre;
            ViewBag.Apellido = apellido;
            ViewBag.NombreCompleto = $"{nombre} {apellido}";
            return View("MostrarDatos2");
        }
    }
}

using System.Diagnostics;
using _2026_1C_Estacionamiento.Models;
using Microsoft.AspNetCore.Mvc;

namespace _2026_1C_Estacionamiento.Controllers
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult Pruebas1()
        {
            return View();
        }

        public IActionResult Pruebas2( int num , string nombre , string apellido)
        {
           ViewBag.Numero = num;
           ViewBag.Nombre = nombre;
           ViewBag.Apellido = apellido;
            Persona persona = new Persona() { Nombre = nombre, Apellido = apellido };
            ViewBag.Persona = persona;
            ViewBag.ListaNombres = new List<string>() { "Juan", "Maria", "Pedro", "Ana" };
           
            return View("PasoParametros",persona);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

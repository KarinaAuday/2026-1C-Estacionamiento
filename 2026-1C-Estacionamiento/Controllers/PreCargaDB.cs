using _2026_1C_Estacionamiento.Data;
using _2026_1C_Estacionamiento.Models;
using Microsoft.AspNetCore.Mvc;

namespace _2026_1C_Estacionamiento.Controllers
{
    public class PreCargaDB : Controller
    {
        private readonly EstacionamientoContext _context;

        public PreCargaDB(EstacionamientoContext context)
        {
            _context = context;
        }

        

        private List<Cliente> clientes = new List<Cliente>
        {
            new Cliente {  Nombre = "Juan", Apellido = "Pérez", Dni = "12345678" , Email ="charly@ort.edu.ar" , Cuit="22222222" , Telefono = "34567890" },
            new Cliente {  Nombre = "Ana", Apellido = "Gómez", Dni = "87654321" ,Email ="Pepe@ort.edu.ar" , Cuit="3333333" ,Telefono = "34567890" },
            new Cliente { Nombre = "Luis", Apellido = "Martínez", Dni = "11223344" , Email ="Alber@ort.edu.ar" , Cuit="4444444" ,Telefono = "34567890"}

        };
        private List<Vehiculo> vehiculos = new List<Vehiculo>()
        {
            new Vehiculo{Patente = 25656588,Marca = "Ford taunus", Color ="Verde" },
            new Vehiculo{Patente = 33333333,Marca = "Chevrolet", Color ="Azul" },
            new Vehiculo{Patente = 44444444,Marca = "Fiat", Color ="Rojo" },
            new Vehiculo{Patente = 55555555,Marca = "Ford", Color ="Verde" },
         
        };

        private void crearVehiculos()
        {
            foreach (var vehiculo in vehiculos)
            {
                // var vehiculoExistente = _context.Vehiculo.FirstOrDefault(v => v.Patente == vehiculo.Patente);
                // if (vehiculoExistente == null)
                // {
                _context.Vehiculos.Add(vehiculo);
                // }
            }
            _context.SaveChanges();
        }
        private void PreCargaClientes()
        {
            foreach (var cliente in clientes)
            {
                // var clienteExistente = _context.Cliente.FirstOrDefault(c => c.Dni == cliente.Dni);
                // if (clienteExistente == null)
                // {
                _context.Cliente.Add(cliente);
                // }
            }
            _context.SaveChanges();
        }
        public IActionResult InicializarBD()
        {
            PreCargaClientes();
            crearVehiculos();
            TempData["PrecargaOK"] = "Base de datos inicializada con datos de prueba.";
            return RedirectToAction("Index", "Home");

        }
    }
}

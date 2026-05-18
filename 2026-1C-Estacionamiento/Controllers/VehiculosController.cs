using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _2026_1C_Estacionamiento.Data;
using _2026_1C_Estacionamiento.Models;

namespace _2026_1C_Estacionamiento.Controllers
{
    public class VehiculosController : Controller
    {
        private readonly EstacionamientoContext _context;

        public VehiculosController(EstacionamientoContext context)
        {
            _context = context;
        }

        // GET: Vehiculos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vehiculos.ToListAsync());
        }

        // GET: Vehiculos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            return View(vehiculo);
        }

        // GET: Vehiculos/Create
        public IActionResult Create()
        {
            if ((bool)(TempData["VieneDeCliente"] = true))
            {
                // Verificar si viene de crear un cliente
                if (TempData["ClienteId"] != null)
                {
                    ViewBag.ClienteId = TempData["ClienteId"];
                    ViewBag.ClienteNombre = TempData["ClienteNombre"];
                    ViewBag.DesdeCliente = true; // Flag para mostrar mensaje en la vista

                    // Mantener en TempData para el POST
                    TempData.Keep("ClienteId");
                    TempData.Keep("ClienteNombre");
                }

            }
            
            return View();
        }

        // POST: Vehiculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Patente,Marca,Color,AnioFabricacion")] Vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehiculo);
                await _context.SaveChangesAsync();
                // Verificar si viene de crear un cliente
                if (TempData["ClienteId"] != null  && (bool)TempData["VieneDeCliente"])
                {
                    int clienteId = (int)TempData["ClienteId"];

                    // Crear la relación ClienteVehiculo
                    var clienteVehiculo = new ClienteVehiculo
                    {
                        ClienteId = clienteId,
                        VehiculoId = vehiculo.Id,
                        ResponsablePrincipal = "Titular", // O lo que necesites por defecto
                        Activo = true
                    };

                    _context.Add(clienteVehiculo);
                    await _context.SaveChangesAsync();

                    TempData["Mensaje"] = "Cliente y vehículo asociados correctamente.";
                }
                TempData.Remove("VieneDeCliente"); // Limpiar el flag después de usarlo
                TempData.Remove("ClienteId"); // Limpiar el ClienteId después de usarlo
                TempData.Remove("ClienteNombre"); // Limpiar el ClienteNombre después de usarlo
                return RedirectToAction(nameof(Index));

            }
            // Si hay error, mantener el ClienteId en ViewBag para la vista
            if (TempData["ClienteId"] != null)
            {
                ViewBag.ClienteId = TempData["ClienteId"];
                ViewBag.ClienteNombre = TempData["ClienteNombre"];
                ViewBag.DesdeCliente = true;
                TempData.Keep("ClienteId");
                TempData.Keep("ClienteNombre");
            }
            return View(vehiculo);
        }

        // GET: Vehiculos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo == null)
            {
                return NotFound();
            }
            return View(vehiculo);
        }

        // POST: Vehiculos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Patente,Marca,Color,AnioFabricacion")] Vehiculo vehiculo)
        {
            if (id != vehiculo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehiculoExists(vehiculo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehiculo);
        }

        // GET: Vehiculos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            return View(vehiculo);
        }

        // POST: Vehiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo != null)
            {
                _context.Vehiculos.Remove(vehiculo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehiculoExists(int id)
        {
            return _context.Vehiculos.Any(e => e.Id == id);
        }
    }
}

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
    public class ClienteVehiculosController : Controller
    {
        private readonly EstacionamientoContext _context;

        public ClienteVehiculosController(EstacionamientoContext context)
        {
            _context = context;
        }

        // GET: ClienteVehiculos
        public async Task<IActionResult> Index()
        {
            var estacionamientoContext = _context.ClienteVehiculo.Include(c => c.Cliente).Include(c => c.Vehiculo);
            return View(await estacionamientoContext.ToListAsync());
        }

        // GET: ClienteVehiculos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteVehiculo = await _context.ClienteVehiculo
                .Include(c => c.Cliente)
                .Include(c => c.Vehiculo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clienteVehiculo == null)
            {
                return NotFound();
            }

            return View(clienteVehiculo);
        }

        // GET: ClienteVehiculos/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Set<Cliente>(), "Id", "Apellido");
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "Id", "Id");
            return View();
        }

        // POST: ClienteVehiculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,VehiculoId,ResponsablePrincipal,Activo")] ClienteVehiculo clienteVehiculo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clienteVehiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Set<Cliente>(), "Id", "Apellido", clienteVehiculo.ClienteId);
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "Id", "Id", clienteVehiculo.VehiculoId);
            return View(clienteVehiculo);
        }

        // GET: ClienteVehiculos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteVehiculo = await _context.ClienteVehiculo.FindAsync(id);
            if (clienteVehiculo == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Set<Cliente>(), "Id", "Apellido", clienteVehiculo.ClienteId);
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "Id", "Id", clienteVehiculo.VehiculoId);
            return View(clienteVehiculo);
        }

        // POST: ClienteVehiculos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,VehiculoId,ResponsablePrincipal,Activo")] ClienteVehiculo clienteVehiculo)
        {
            if (id != clienteVehiculo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clienteVehiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteVehiculoExists(clienteVehiculo.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Set<Cliente>(), "Id", "Apellido", clienteVehiculo.ClienteId);
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "Id", "Id", clienteVehiculo.VehiculoId);
            return View(clienteVehiculo);
        }

        // GET: ClienteVehiculos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteVehiculo = await _context.ClienteVehiculo
                .Include(c => c.Cliente)
                .Include(c => c.Vehiculo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clienteVehiculo == null)
            {
                return NotFound();
            }

            return View(clienteVehiculo);
        }

        // POST: ClienteVehiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clienteVehiculo = await _context.ClienteVehiculo.FindAsync(id);
            if (clienteVehiculo != null)
            {
                _context.ClienteVehiculo.Remove(clienteVehiculo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteVehiculoExists(int id)
        {
            return _context.ClienteVehiculo.Any(e => e.Id == id);
        }
    }
}

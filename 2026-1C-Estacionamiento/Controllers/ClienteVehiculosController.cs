using _2026_1C_Estacionamiento.Data;
using _2026_1C_Estacionamiento.Models;
using _2026_1C_Estacionamiento.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2026_1C_Estacionamiento.Controllers
{
    public class ClienteVehiculosController : Controller
    {
        private readonly EstacionamientoContext _context;

        public ClienteVehiculosController(EstacionamientoContext context)
        {
            _context = context;
        }

        //// GET: ClienteVehiculos
        //public async Task<IActionResult> Index()
        //{
        //    var estacionamientoContext = _context.ClienteVehiculo.Include(c => c.Cliente).Include(c => c.Vehiculo);
        //    return View(await estacionamientoContext.ToListAsync());
        //}



        // GET: ClienteVehiculo (Lista todos los clientes con sus vehículos)
        public async Task<IActionResult> Index()
        {
            // Obtener todos los clientes con sus relaciones de vehículos
            var clientes = await _context.Cliente
                .Include(c => c.ClienteVehiculos)
                    .ThenInclude(cv => cv.Vehiculo)
                .ToListAsync();

            return View(clientes);
        }

        // GET: ClienteVehiculo/EditarVehiculos/5
        public async Task<IActionResult> EditarVehiculos(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .Include(c => c.ClienteVehiculos)
                    .ThenInclude(cv => cv.Vehiculo)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            // Obtener IDs de vehículos ya asociados a este cliente
            var vehiculosAsociadosIds = cliente.ClienteVehiculos
                .Where(cv => cv.Activo)
                .Select(cv => cv.VehiculoId)
                .ToList();

            // Obtener todos los vehículos para el dropdown
            var todosVehiculos = await _context.Vehiculos.ToListAsync();

            // Crear el ViewModel
            var viewModel = new EditarVehiculosViewModel
            {
                ClienteId = cliente.Id,
                ClienteNombre = $"{cliente.Nombre} {cliente.Apellido}",
                VehiculosAsociados = cliente.ClienteVehiculos
                    .Where(cv => cv.Activo)
                    .Select(cv => new VehiculoAsociadoViewModel
                    {
                        ClienteVehiculoId = cv.Id,
                        VehiculoId = cv.VehiculoId,
                        Patente = cv.Vehiculo.Patente.ToString(),
                        Marca = cv.Vehiculo.Marca,
                        Color = cv.Vehiculo.Color,
                        ResponsablePrincipal = cv.ResponsablePrincipal
                    }).ToList(),
                VehiculosDisponibles = new SelectList(
                    todosVehiculos.Where(v => !vehiculosAsociadosIds.Contains(v.Id)),
                    "Id",
                    "Patente"
                )
            };

            return View(viewModel);
        }

        // POST: ClienteVehiculo/AsociarVehiculo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AsociarVehiculo(int clienteId, int vehiculoId, string responsablePrincipal)
        {
            // Verificar si ya existe la relación (incluso inactiva)
            var relacionExistente = await _context.ClienteVehiculo
                .FirstOrDefaultAsync(cv => cv.ClienteId == clienteId && cv.VehiculoId == vehiculoId);

            if (relacionExistente != null)
            {
                // Reactivar si estaba inactiva
                if (!relacionExistente.Activo)
                {
                    relacionExistente.Activo = true;
                    relacionExistente.ResponsablePrincipal = responsablePrincipal ?? "Titular";
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Vehículo reactivado correctamente.";
                }
                else
                {
                    TempData["Error"] = "Este vehículo ya está asociado al cliente.";
                }
            }
            else
            {
                // Crear nueva relación
                var clienteVehiculo = new ClienteVehiculo
                {
                    ClienteId = clienteId,
                    VehiculoId = vehiculoId,
                    ResponsablePrincipal = responsablePrincipal ?? "Titular",
                    Activo = true
                };

                _context.ClienteVehiculo.Add(clienteVehiculo);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Vehículo asociado correctamente.";
            }

            return RedirectToAction(nameof(EditarVehiculos), new { id = clienteId });
        }

        // POST: ClienteVehiculo/DesasociarVehiculo/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DesasociarVehiculo(int id, int clienteId)
        {
            var clienteVehiculo = await _context.ClienteVehiculo.FindAsync(id);

            if (clienteVehiculo == null)
            {
                return NotFound();
            }

            // Marcar como inactivo en lugar de eliminar (soft delete)
            clienteVehiculo.Activo = false;
            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Vehículo desasociado correctamente.";
            return RedirectToAction(nameof(EditarVehiculos), new { id = clienteId });
        }

        // POST: ClienteVehiculo/ActualizarResponsable
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarResponsable(int id, string responsablePrincipal, int clienteId)
        {
            var clienteVehiculo = await _context.ClienteVehiculo.FindAsync(id);

            if (clienteVehiculo == null)
            {
                return NotFound();
            }

            clienteVehiculo.ResponsablePrincipal = responsablePrincipal;
            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Responsable actualizado correctamente.";
            return RedirectToAction(nameof(EditarVehiculos), new { id = clienteId });
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestorVentas_FLOMA.Models;

namespace GestorVentas_FLOMA.Controllers
{
    public class VentumsController : Controller
    {
        private readonly FlomaContext _context;

        public VentumsController(FlomaContext context)
        {
            _context = context;
        }

        // GET: Ventums
        public async Task<IActionResult> Index()
        {
            var flomaContext = _context.Venta.Include(v => v.IdClienteNavigation).Include(v => v.IdDescuentoNavigation).Include(v => v.IdDetalleProductoNavigation).Include(v => v.IdVendedorNavigation);
            return View(await flomaContext.ToListAsync());
        }

        // GET: Ventums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventum = await _context.Venta
                .Include(v => v.IdClienteNavigation)
                .Include(v => v.IdDescuentoNavigation)
                .Include(v => v.IdDetalleProductoNavigation)
                .Include(v => v.IdVendedorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ventum == null)
            {
                return NotFound();
            }

            return View(ventum);
        }

        // GET: Ventums/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id");
            ViewData["IdDescuento"] = new SelectList(_context.Descuentos, "Id", "Id");
            ViewData["IdDetalleProducto"] = new SelectList(_context.Detalleproductos, "IdDetalle", "IdDetalle");
            ViewData["IdVendedor"] = new SelectList(_context.Vendedors, "Id", "Id");
            return View();
        }

        // POST: Ventums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,IdDetalleProducto,IdCliente,Precio,FechaEntrega,DomicilioEntrega,IdDescuento,Cantidad,Complete,Active,IdVendedor")] Ventum ventum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ventum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id", ventum.IdCliente);
            ViewData["IdDescuento"] = new SelectList(_context.Descuentos, "Id", "Id", ventum.IdDescuento);
            ViewData["IdDetalleProducto"] = new SelectList(_context.Detalleproductos, "IdDetalle", "IdDetalle", ventum.IdDetalleProducto);
            ViewData["IdVendedor"] = new SelectList(_context.Vendedors, "Id", "Id", ventum.IdVendedor);
            return View(ventum);
        }

        // GET: Ventums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventum = await _context.Venta.FindAsync(id);
            if (ventum == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id", ventum.IdCliente);
            ViewData["IdDescuento"] = new SelectList(_context.Descuentos, "Id", "Id", ventum.IdDescuento);
            ViewData["IdDetalleProducto"] = new SelectList(_context.Detalleproductos, "IdDetalle", "IdDetalle", ventum.IdDetalleProducto);
            ViewData["IdVendedor"] = new SelectList(_context.Vendedors, "Id", "Id", ventum.IdVendedor);
            return View(ventum);
        }

        // POST: Ventums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,IdDetalleProducto,IdCliente,Precio,FechaEntrega,DomicilioEntrega,IdDescuento,Cantidad,Complete,Active,IdVendedor")] Ventum ventum)
        {
            if (id != ventum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ventum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentumExists(ventum.Id))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id", ventum.IdCliente);
            ViewData["IdDescuento"] = new SelectList(_context.Descuentos, "Id", "Id", ventum.IdDescuento);
            ViewData["IdDetalleProducto"] = new SelectList(_context.Detalleproductos, "IdDetalle", "IdDetalle", ventum.IdDetalleProducto);
            ViewData["IdVendedor"] = new SelectList(_context.Vendedors, "Id", "Id", ventum.IdVendedor);
            return View(ventum);
        }

        // GET: Ventums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventum = await _context.Venta
                .Include(v => v.IdClienteNavigation)
                .Include(v => v.IdDescuentoNavigation)
                .Include(v => v.IdDetalleProductoNavigation)
                .Include(v => v.IdVendedorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ventum == null)
            {
                return NotFound();
            }

            return View(ventum);
        }

        // POST: Ventums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ventum = await _context.Venta.FindAsync(id);
            if (ventum != null)
            {
                _context.Venta.Remove(ventum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentumExists(int id)
        {
            return _context.Venta.Any(e => e.Id == id);
        }
        public IActionResult _Create()
        {
            var productos = _context.Detalleproductos
                .Select(p => new {
                    Id = p.IdDetalle,
                    NombreDescripcion = $"{p.IdProductoNavigation.Nombre} - {p.Descripcion}",
                })
                .ToList();

            var clientes = _context.Clientes
                .Select(c => new {
                    Id = c.Id,
                    Nombre = c.Nombre,
                })
                .ToList();

            ViewBag.Clientes = new SelectList(clientes, "Id", "Nombre");
            ViewBag.Vendedores = new SelectList(_context.Vendedors, "Id", "Nombre");
            ViewBag.DetalleProductos = new SelectList(productos, "Id", "NombreDescripcion");

            return PartialView("_Create");
        }

    }
}

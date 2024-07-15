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
    public class ProductoController : Controller
    {
        private readonly FlomaContext _context;

        public ProductoController(FlomaContext context)
        {
            _context = context;
        }

        // GET: Detalleproductoes
        public async Task<IActionResult> Index()
        {
            var flomaContext = _context.Detalleproductos.Include(d => d.IdProductoNavigation);
            return View(await flomaContext.ToListAsync());
        }

        // GET: Detalleproductoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleproducto = await _context.Detalleproductos
                .Include(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdDetalle == id);
            if (detalleproducto == null)
            {
                return NotFound();
            }

            return View(detalleproducto);
        }
        [HttpPost]
        public decimal ObtenerPrecioProducto(int idProducto)
        {
            // Aquí debes escribir la lógica para obtener el precio del producto según su ID
            var cliente = _context.Detalleproductos.FirstOrDefault(p => p.IdDetalle == idProducto);

            if (cliente != null && cliente.PrecioVenta != null)
            {
                // Suponiendo que el precio está almacenado en la propiedad PrecioVenta
                return cliente.PrecioVenta.Value;
            }
            else
            {
                return 0; // O cualquier otro valor por defecto que decidas
            }
        }
        // GET: Detalleproductoes/Create
        public IActionResult Create()
        {
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");
            return View();
        }

        // POST: Detalleproductoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetalle,IdProducto,Descripcion,Costo,PrecioVenta,Img,Active")] Detalleproducto detalleproducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleproducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detalleproducto.IdProducto);
            return View(detalleproducto);
        }

        // GET: Detalleproductoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleproducto = await _context.Detalleproductos.FindAsync(id);
            if (detalleproducto == null)
            {
                return NotFound();
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detalleproducto.IdProducto);
            return View(detalleproducto);
        }

        // POST: Detalleproductoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDetalle,IdProducto,Descripcion,Costo,PrecioVenta,Img,Active")] Detalleproducto detalleproducto)
        {
            if (id != detalleproducto.IdDetalle)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleproducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleproductoExists(detalleproducto.IdDetalle))
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
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detalleproducto.IdProducto);
            return View(detalleproducto);
        }

        // GET: Detalleproductoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleproducto = await _context.Detalleproductos
                .Include(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdDetalle == id);
            if (detalleproducto == null)
            {
                return NotFound();
            }

            return View(detalleproducto);
        }

        // POST: Detalleproductoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalleproducto = await _context.Detalleproductos.FindAsync(id);
            if (detalleproducto != null)
            {
                _context.Detalleproductos.Remove(detalleproducto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleproductoExists(int id)
        {
            return _context.Detalleproductos.Any(e => e.IdDetalle == id);
        }
    }
}

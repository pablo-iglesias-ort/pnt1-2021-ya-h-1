using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarritoCompras.Data;
using CarritoCompras.Models;

namespace CarritoCompras.Controllers
{
    public class CarritoController : Controller
    {
        private readonly CarritoComprasContext _context;

        public CarritoController(CarritoComprasContext context)
        {
            _context = context;
        }

        // GET: Carrito
        public async Task<IActionResult> Index()
        {
            return View(await _context.Carrito.ToListAsync());            
        }

        // GET: Carrito/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrito =  await _context.Carrito.FirstOrDefaultAsync(e => e.Id == id);

            if (carrito == null)
            {
                return NotFound();
            }

            return View(carrito);
        }

        // GET: Carrito/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carrito/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Activo,Subtotal")] Carrito carrito)
        {
            if (ModelState.IsValid)
            {
                carrito.Id = Guid.NewGuid();
                _context.Add(carrito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carrito);
        }

        // GET: Carrito/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrito = BuscarCarrito(id);

            if (carrito == null)
            {
                return NotFound();
            }
            return View(carrito);
        }

        // POST: Carrito/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Activo,Subtotal")] Carrito carrito)
        {
            if (id != carrito.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var carritoEncontrado = BuscarCarrito(id);
                    carritoEncontrado.Activo = carrito.Activo;
                    carritoEncontrado.Subtotal = carrito.Subtotal;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarritoExists(carrito.Id))
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
            return View(carrito);
        }

        // GET: Carrito/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carrito
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carrito == null)
            {
                return NotFound();
            }

            return View(carrito);
        }

        // POST: Carrito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var carrito = await _context.Carrito.FindAsync(id);
            _context.Carrito.Remove(carrito);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarritoExists(Guid id)
        {
            return _context.Carrito.Any(e => e.Id == id);
        }

        private Carrito BuscarCarrito(Guid? id)
        {
            return _context.Carrito.FirstOrDefault(e => e.Id == id);
        }
    }
}

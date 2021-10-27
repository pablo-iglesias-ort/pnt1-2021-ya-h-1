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
    public class CarritoItemController : Controller
    {
        private readonly CarritoComprasContext _context;
        
        public CarritoItemController(CarritoComprasContext context)
        {
            _context = context;
        }

        // GET: CarritoItem
        public async Task<IActionResult> Index()
        {
            return View(await _context.CarritoItem.ToListAsync());
            
        }

        // GET: CarritoItem/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carritoItem = await _context.CarritoItem.FirstOrDefaultAsync(e => e.Id == id);
            if (carritoItem == null)
            {
                return NotFound();
            }

            return View(carritoItem);
        }

        // GET: CarritoItem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarritoItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ValorUnitario,Cantidad,Subtotal")] CarritoItem carritoItem)
        {
            if (ModelState.IsValid)
            {
                carritoItem.Id = Guid.NewGuid();
                _context.Add(carritoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carritoItem);
        }

        // GET: CarritoItem/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carritoItem = await _context.CarritoItem.FirstOrDefaultAsync(e => e.Id == id);
            if (carritoItem == null)
            {
                return NotFound();
            }
            return View(carritoItem);
        }

        // POST: CarritoItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ValorUnitario,Cantidad,Subtotal")] CarritoItem carritoItem)
        {
            if (id != carritoItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var carritoItemEncontrado = await _context.CarritoItem.FirstOrDefaultAsync(e => e.Id == id);
                    carritoItemEncontrado.ValorUnitario = carritoItem.ValorUnitario;
                    carritoItemEncontrado.Cantidad = carritoItem.Cantidad;
                    carritoItemEncontrado.Subtotal = carritoItem.Subtotal;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarritoItemExists(carritoItem.Id))
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
            return View(carritoItem);
        }

        // GET: CarritoItem/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carritoItem = await _context.CarritoItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carritoItem == null)
            {
                return NotFound();
            }

            return View(carritoItem);
        }

        // POST: CarritoItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var carritoItem = await _context.CarritoItem.FindAsync(id);
            _context.CarritoItem.Remove(carritoItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarritoItemExists(Guid id)
        {
            return _context.CarritoItem.Any(e => e.Id == id);
        }        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarritoCompras.Data;
using CarritoCompras.Models;
using Microsoft.AspNetCore.Authorization;

namespace CarritoCompras.Controllers
{
    public class CarritosController : Controller
    {
        private readonly CarritoComprasContext _context;

        public CarritosController(CarritoComprasContext context)
        {
            _context = context;
        }

        // GET: Carritos
        public async Task<IActionResult> Index()
        {
            var carritoComprasContext = _context.Carrito.Include(c => c.Cliente);
            return View(await carritoComprasContext.ToListAsync());
        }

        // GET: Carritos
        public async Task<IActionResult> CarritoCliente(Guid Id)
        {            
            var carrito = _context.Carrito.Include(n => n.CarritosItems)
                                            .ThenInclude(ci => ci.Producto)
                                          .Include(c => c.Cliente)
                                          .FirstOrDefaultAsync(n => n.ClienteId == Id && n.Activo);
            return View(await carrito);
        }



        // GET: Carritos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carrito
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.CarritoId == id);
            if (carrito == null)
            {
                return NotFound();
            }

            return View(carrito);
        }

        // GET: Carritos/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Apellido");
            return View();
        }

        // POST: Carritos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarritoId,ClienteId,Activo,Subtotal")] Carrito carrito)
        {
            if (ModelState.IsValid)
            {
                carrito.CarritoId = Guid.NewGuid();
                _context.Add(carrito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Apellido", carrito.ClienteId);
            return View(carrito);
        }

        // POST: Carritos/Agregar/5
        // Agrega un CarritoItem al Carrito
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Agregar(Producto producto, int Cantidad)
        {
            if(Cantidad >= 1)
            {
                var usuarioId = Guid.Parse(User.FindFirst("IdDeUsuario").Value);

                var carrito = await _context.Carrito.FirstOrDefaultAsync(p => p.ClienteId == usuarioId && p.Activo);

                var carritoItem = new CarritoItem();
                carritoItem.Cantidad = Cantidad;
                carritoItem.ProductoId = producto.ProductoId;
                carritoItem.CarritoId = carrito.CarritoId;

                _context.CarritoItem.Add(carritoItem);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(CarritoCliente), new { id = carrito.ClienteId });
            }
            TempData["error"] = "Ingrese 1 o más productos";
            return RedirectToAction("Agregar", "Productos", new { id = producto.ProductoId});
        }

        // GET: Carritos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carrito.FindAsync(id);
            if (carrito == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Apellido", carrito.ClienteId);
            return View(carrito);
        }

        // POST: Carritos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CarritoId,ClienteId,Activo,Subtotal")] Carrito carrito)
        {
            if (id != carrito.CarritoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carrito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarritoExists(carrito.CarritoId))
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
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Apellido", carrito.ClienteId);
            return View(carrito);
        }

        // GET: Carritos/Edit/5
        public async Task<IActionResult> EditarItem(Guid? id, int Cantidad)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carritoItem = await _context.CarritoItem
                .Include(n => n.Producto)
                .Include(n => n.Carrito)
                .FirstOrDefaultAsync(n => n.CarritoItemId == id);
            if (carritoItem == null)
            {
                return NotFound();
            }
            ViewData["Cantidad"] = Cantidad;
            return View(carritoItem);
        }

        // POST: Carritos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarItem(Guid id, CarritoItem carritoItem)
        {
            if (id != carritoItem.CarritoItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carritoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarritoExists(carritoItem.CarritoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var carrito = await _context.Carrito
                    .FirstOrDefaultAsync(m => m.CarritoId == carritoItem.CarritoId);
                return RedirectToAction(nameof(CarritoCliente), new { id = carrito.ClienteId });
            }

            return View(id);
        }

        // GET: Carritos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carrito
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.CarritoId == id);
            if (carrito == null)
            {
                return NotFound();
            }

            return View(carrito);
        }

        // POST: Carritos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var carrito = await _context.Carrito.FindAsync(id);
            _context.Carrito.Remove(carrito);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Carritos/Delete/5
        public async Task<IActionResult> RemoveItem(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carritoItem = await _context.CarritoItem
                .Include(c => c.Producto)
                .Include(c => c.Carrito)
                .FirstOrDefaultAsync(m => m.CarritoItemId == id);
            if (carritoItem == null)
            {
                return NotFound();
            }

            return View(carritoItem);
        }

        // POST: Carritos/Delete/5
        [HttpPost, ActionName("RemoveItem")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveItem1(Guid id)
        {
            var carritoItem = await _context.CarritoItem
                  .FirstOrDefaultAsync(m => m.CarritoItemId == id);
            var carrito = await _context.Carrito
                .FirstOrDefaultAsync(m => m.CarritoId == carritoItem.CarritoId);
            _context.CarritoItem.Remove(carritoItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(CarritoCliente),new { id =carrito.ClienteId});
        }

        private bool CarritoExists(Guid id)
        {
            return _context.Carrito.Any(e => e.CarritoId == id);
        }

        // GET: VACIAR EL CARRITO
        public async Task<IActionResult> Vaciar(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carrito.Include(c => c.CarritosItems)
                .FirstOrDefaultAsync(m => m.CarritoId== id);
            if (carrito == null)
            {
                return NotFound();
            }
            carrito.CarritosItems.Clear();
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(CarritoCliente), new { id = carrito.ClienteId });
        }
    }
}

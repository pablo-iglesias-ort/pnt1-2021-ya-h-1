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
    public class SucursalesController : Controller
    {
        private readonly CarritoComprasContext _context;

        public SucursalesController(CarritoComprasContext context)
        {
            _context = context;
        }

        // GET: Sucursales
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sucursal.ToListAsync());
        }

        // GET: MOstrar productos para agregar al Stock de la Sucursal
        public async Task<IActionResult> AgregarProductos(Guid id)
        {
            var categorias = _context.Categoria.Include(n => n.Productos);
            ViewData["Stock"] = _context.StockItem.Where(x => x.SucursalId == id).ToList();
            TempData["sucursal"] = id;
            if(TempData["error"] != null)
            {
                ViewBag.Error= TempData["error"];
            }
            return View(await categorias.ToListAsync());
        }

        // GET: Sucursales para hacer una COMPRA
        public async Task<IActionResult> SucursalCompra(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.CarritoId = id;
             if(TempData["error"] != null)
            {
                ViewBag.Error = TempData["error"];
            }
            return View(await _context.Sucursal.ToListAsync());
        }

        // GET: Sucursales/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sucursal = await _context.Sucursal
                .FirstOrDefaultAsync(m => m.SucursalId == id);
            if (sucursal == null)
            {
                return NotFound();
            }

            return View(sucursal);
        }

        // GET: Sucursales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sucursales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SucursalId,Nombre,Direccion,Telefono,Email")] Sucursal sucursal)
        {
            if (ModelState.IsValid)
            {
                sucursal.SucursalId = Guid.NewGuid();
                _context.Add(sucursal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sucursal);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarStock(int cantidad, Guid idproducto, Guid sucursal)
        {
            if(cantidad <= 0)
            {
                TempData["error"] = "La cantidad debe ser mayor a 0";

            }
            else 
            { 
                var suc = await _context.Sucursal
                         .Include(c => c.StockItems)
                             .ThenInclude(ci => ci.Producto)
                     .FirstOrDefaultAsync(m => m.SucursalId == sucursal);
                if(suc.StockItems.Find(x => x.ProductoId == idproducto) != null)
                {
                    //existe
                    var stkitem = suc.StockItems.Find(x => x.ProductoId == idproducto);
                    stkitem.Cantidad += cantidad;
                    _context.StockItem.Update(stkitem);
                }
                else
                {
                    var stkitem = new StockItem();
                    stkitem.ProductoId = idproducto;
                    stkitem.SucursalId = sucursal;
                    stkitem.Cantidad = cantidad;
                    stkitem.StockItemId = Guid.NewGuid();
                    _context.StockItem.Add(stkitem);
                    suc.StockItems.Add(stkitem);
                    _context.Sucursal.Update(suc);
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(AgregarProductos), new { id = sucursal });
        }
        // GET: Sucursales/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sucursal = await _context.Sucursal.FindAsync(id);
            if (sucursal == null)
            {
                return NotFound();
            }
            return View(sucursal);
        }

        // POST: Sucursales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SucursalId,Nombre,Direccion,Telefono,Email")] Sucursal sucursal)
        {
            if (id != sucursal.SucursalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sucursal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SucursalExists(sucursal.SucursalId))
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
            return View(sucursal);
        }

        // GET: Sucursales/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sucursal = await _context.Sucursal
                .FirstOrDefaultAsync(m => m.SucursalId == id);
            if (sucursal == null)
            {
                return NotFound();
            }

            return View(sucursal);
        }

        // POST: Sucursales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var sucursal = await _context.Sucursal.FindAsync(id);
            _context.Sucursal.Remove(sucursal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SucursalExists(Guid id)
        {
            return _context.Sucursal.Any(e => e.SucursalId == id);
        }
    }
}

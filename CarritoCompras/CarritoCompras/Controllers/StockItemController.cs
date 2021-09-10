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
    public class StockItemController : Controller
    {
        private readonly CarritoComprasContext _context;
        static List<StockItem> stockItems = new List<StockItem>()
        {
            new StockItem()
            {
                Id = Guid.NewGuid(),
                Sucursal = new Sucursal()
            {
                Id = Guid.NewGuid(),
                Nombre = "Boedo",
                Direccion = "Av Boedo 264",
                Telefono = "5555-5555",
                Email ="sucursal@mail.com",
                StockItems = null
    },
                Producto = null,
                Cantidad = 23,
                
    },
            new StockItem()
            {
                Id = Guid.NewGuid(),
                Sucursal = new Sucursal()
            {
                Id = Guid.NewGuid(),
                Nombre = "Boedo",
                Direccion = "Av Boedo 264",
                Telefono = "5555-5555",
                Email ="sucursal@mail.com",
                StockItems = null
    },
                Producto = null,
                Cantidad = 23,
                },
        };
        public StockItemController(CarritoComprasContext context)
        {
            _context = context;
        }

        // GET: StockItem
        public async Task<IActionResult> Index()
        {
            //return View(await _context.StockItem.ToListAsync());
            return View(stockItems);
        }

        // GET: StockItem/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockItem = BuscarStockItem(id);
            if (stockItem == null)
            {
                return NotFound();
            }

            return View(stockItem);
        }

        // GET: StockItem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StockItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cantidad")] StockItem stockItem)
        {
            if (ModelState.IsValid)
            {
                stockItem.Id = Guid.NewGuid();
                _context.Add(stockItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockItem);
        }

        // GET: StockItem/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockItem = BuscarStockItem(id);
            if (stockItem == null)
            {
                return NotFound();
            }
            return View(stockItem);
        }

        // POST: StockItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Cantidad")] StockItem stockItem)
        {
            if (id != stockItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var stockItemEncontrado = BuscarStockItem(id);
                    stockItemEncontrado.Cantidad = stockItem.Cantidad;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockItemExists(stockItem.Id))
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
            return View(stockItem);
        }

        // GET: StockItem/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockItem = await _context.StockItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stockItem == null)
            {
                return NotFound();
            }

            return View(stockItem);
        }

        // POST: StockItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var stockItem = await _context.StockItem.FindAsync(id);
            _context.StockItem.Remove(stockItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockItemExists(Guid id)
        {
            return _context.StockItem.Any(e => e.Id == id);
        }
        private StockItem BuscarStockItem(Guid? id)
        {
            var stockItem = stockItems.FirstOrDefault(m => m.Id == id);
            return stockItem;
        }
    }
}

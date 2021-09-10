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
    public class SucursalController : Controller
    {
        private readonly CarritoComprasContext _context;
        static List<Sucursal> sucursales = new List<Sucursal>()
        {
            new Sucursal()
            {
                Id = Guid.NewGuid(),
                Nombre = "Boedo",
                Direccion = "Av Boedo 264",
                Telefono = "5555-5555",
                Email ="sucursal@mail.com",
                StockItems = null
    },
            new Sucursal()
            {
                Id = Guid.NewGuid(),
                Nombre = "LaPlata",
                Direccion = "Av La Palta 1356",
                Telefono = "5555-5555",
                Email ="sucursal2@mail.com",
                StockItems = null
    }
        };
        public SucursalController(CarritoComprasContext context)
        {
            _context = context;
        }

        // GET: Sucursal
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Sucursal.ToListAsync());
            return View(sucursales);
        }

        // GET: Sucursal/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sucursal = BuscarSucursal(id);
            if (sucursal == null)
            {
                return NotFound();
            }

            return View(sucursal);
        }

        // GET: Sucursal/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sucursal/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Direccion,Telefono,Email")] Sucursal sucursal)
        {
            if (ModelState.IsValid)
            {
                sucursal.Id = Guid.NewGuid();
                _context.Add(sucursal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sucursal);
        }

        // GET: Sucursal/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sucursal = BuscarSucursal(id);
            if (sucursal == null)
            {
                return NotFound();
            }
            return View(sucursal);
        }

        // POST: Sucursal/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nombre,Direccion,Telefono,Email")] Sucursal sucursal)
        {
            if (id != sucursal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var sucursalEncontrada = BuscarSucursal(id);
                    sucursalEncontrada.Nombre = sucursal.Nombre;
                    sucursalEncontrada.Direccion = sucursal.Direccion;
                    sucursalEncontrada.Telefono = sucursal.Telefono;
                    sucursalEncontrada.Email = sucursal.Email;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SucursalExists(sucursal.Id))
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

        // GET: Sucursal/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sucursal = await _context.Sucursal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sucursal == null)
            {
                return NotFound();
            }

            return View(sucursal);
        }

        // POST: Sucursal/Delete/5
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
            return _context.Sucursal.Any(e => e.Id == id);
        }

        private Sucursal BuscarSucursal(Guid? id)
        {
            var sucursal = sucursales.FirstOrDefault(m => m.Id == id);
            return sucursal;
        }
    }
}

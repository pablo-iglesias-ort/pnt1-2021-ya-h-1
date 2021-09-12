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
    public class ProductoController : Controller
    {
        private readonly CarritoComprasContext _context;
        static List<Producto> productos = new List<Producto>()
        {
            new Producto
            {
                Id = Guid.NewGuid(),
                Nombre = "Harry Potter y la priedra filosofal",
                Descripcion = "Harry Potter Libro 1",
                PrecioVigente = 2000.00,
                Activo = true,
                Categoria = null
            },
            new Producto
            {
                Id = Guid.NewGuid(),
                Nombre = "Harry Potter y la camara secreta",
                Descripcion = "Harry Potter Libro 2",
                PrecioVigente = 2000.00,
                Activo = true,
                Categoria = null
            },
            new Producto
            {
                Id = Guid.NewGuid(),
                Nombre = "Max Steel",
                Descripcion = "Juguete Max Steel",
                PrecioVigente = 4999.99,
                Activo = true,
                Categoria = null
            },
            new Producto
            {
                Id = Guid.NewGuid(),
                Nombre = "Paw Patrol",
                Descripcion = "Paw Patrol autitos tematicos",
                PrecioVigente = 1499.99,
                Activo = true,
                Categoria = null
            }
        };
        public ProductoController(CarritoComprasContext context)
        {
            _context = context;
        }
        public void CargarProductos()
        {
            foreach (Producto p in productos)
            {
                _context.Producto.Add(p);
            }
        }
        // GET: Productos
        public async Task<IActionResult> Index()
        {
            return View(productos);
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = productos.FirstOrDefault(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            new CategoriaController(_context).CargarCategorias();
            ViewBag.Types = new SelectList(_context.Categoria.Local.ToList(), "Id", "Nombre", "0");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Descripcion,PrecioVigente,Activo,Categoria")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                producto.Id = Guid.NewGuid();
                _context.Add(producto);
                Categoria c = producto.Categoria;
                c.Productos.Add(producto);
                _context.Update(c);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CargarProductos();
            var producto = _context.Producto.Local.ToList().Find(p => p.Id == id);
            if (producto == null)
            {
                return NotFound();
            }
            new CategoriaController(_context).CargarCategorias();
            ViewBag.Types = new SelectList(_context.Categoria.Local.ToList(), "Id", "Nombre", "0");

            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nombre,Descripcion,PrecioVigente,Activo,Categoria")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Producto p = _context.Producto.Local.ToList().Where(p => p.Id == producto.Id).FirstOrDefault();
                    producto.Categoria.Productos.Remove(p);
                    _context.Update(producto);
                    Categoria c = producto.Categoria;
                    c.Productos.Add(producto);
                    _context.Update(c);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
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
            return View(producto);
        }

        // GET: Productos/Delete/5 - NO SE ELIMINAN PRODUCTOS
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var producto = await _context.Producto
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (producto == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(producto);
        //}

        //// POST: Productos/Delete/5 - NO SE ELIMINAN PRODUCTOS
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var producto = await _context.Producto.FindAsync(id);
        //    _context.Producto.Remove(producto);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool ProductoExists(Guid id)
        {
            return _context.Producto.Any(e => e.Id == id);
        }
    }
}

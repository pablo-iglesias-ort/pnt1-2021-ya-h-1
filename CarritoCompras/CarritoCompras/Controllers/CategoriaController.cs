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
    public class CategoriaController : Controller
    {
        private readonly CarritoComprasContext _context;
        
        public CategoriaController(CarritoComprasContext context)
        {
            _context = context;
        }

        // GET: Categoria
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categoria.ToListAsync());
        }

        // GET: Categoria/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Categoria/Create
        public IActionResult Create()
        {
            new ProductoController(_context).CargarProductos();
            ViewBag.Types = new SelectList(_context.Producto.Local.ToList().Where<Producto>(p => p.Categoria == null), "Id", "Nombre", "0");
            return View();
        }

        // POST: Categoria/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Descripcion,Productos")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                categoria.Id = Guid.NewGuid();
                _context.Add(categoria);
                foreach (Producto p in categoria.Productos)
                {
                    p.Categoria = categoria;
                    _context.Update(p);
                }
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Categoria/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CargarCategorias();
            var categoria = _context.Categoria.Local.ToList().Find(c => c.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }
            new ProductoController(_context).CargarProductos();
            ViewBag.Types = new SelectList(_context.Producto.Local.ToList().Where(p => p.Categoria == null || (p.Categoria != null && p.Categoria.Id == id)), "Id", "Nombre", "0");
            return View(categoria);
        }

        // POST: Categoria/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nombre,Descripcion,Productos")] Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    foreach (Producto p in categoria.Productos){
                        p.Categoria = categoria;
                        _context.Update(p);
                    }
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.Id))
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
            return View(categoria);
        }

        // GET: Categoria/Delete/5 -NO SE ELIMINAN LAS CATEGORIAS
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var categoria = await _context.Categoria
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (categoria == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(categoria);
        //}

        // POST: Categoria/Delete/5 - NO SE ELIMINAN
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var categoria = await _context.Categoria.FindAsync(id);
        //    _context.Categoria.Remove(categoria);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool CategoriaExists(Guid id)
        {
            return _context.Categoria.Any(e => e.Id == id);
        }
    }
}

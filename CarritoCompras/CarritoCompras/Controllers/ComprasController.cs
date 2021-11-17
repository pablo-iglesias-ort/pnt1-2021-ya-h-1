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
    public class ComprasController : Controller
    {
        private readonly CarritoComprasContext _context;

        public ComprasController(CarritoComprasContext context)
        {
            _context = context;
        }

        // GET: Compras
        public async Task<IActionResult> Index()
        {
            var carritoComprasContext = _context.Compra.Include(c => c.Carrito).Include(c => c.Cliente).Include(c => c.Sucursal);
            return View(await carritoComprasContext.ToListAsync());
        }

        // GET: Compras del Cliente
        public async Task<IActionResult> ComprasCliente(Guid id)
        {
            var cliente = _context.Cliente.FirstOrDefault(n => n.Id == id);
            ViewData["nombre"] = cliente.Nombre + cliente.Apellido;
            var carritoComprasContext = _context.Compra.Include(c => c.Carrito).Include(c => c.Cliente).Include(c => c.Sucursal).Where(n => n.ClienteId == id);
            return View(await carritoComprasContext.ToListAsync());
        }


        

        // GET: Compras/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compra
                .Include(c => c.Carrito)
                    .ThenInclude(ci => ci.CarritosItems)
                        .ThenInclude(p => p.Producto)
                .Include(c => c.Cliente)
                .Include(c => c.Sucursal)
                .FirstOrDefaultAsync(m => m.CompraId == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // GET: Compras/Create
        public IActionResult Create()
        {
            ViewData["CarritoId"] = new SelectList(_context.Carrito, "CarritoId", "CarritoId");
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Apellido");
            ViewData["SucursalId"] = new SelectList(_context.Sucursal, "SucursalId", "Direccion");
            return View();
        }

        // POST: Compras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompraId,ClienteId,CarritoId,Total,Fecha,SucursalId")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                compra.CompraId = Guid.NewGuid();
                _context.Add(compra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarritoId"] = new SelectList(_context.Carrito, "CarritoId", "CarritoId", compra.CarritoId);
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Apellido", compra.ClienteId);
            ViewData["SucursalId"] = new SelectList(_context.Sucursal, "SucursalId", "Direccion", compra.SucursalId);
            return View(compra);
        }

        // GET: GENERAR UNA COMPRA NUEVA A PARTIR DE UN CARRITO Y UNA SUCURSAL    
        public async Task<IActionResult> Comprar(Guid sucursalid, Guid carritoid)
        {
        /*if (1==1)
        {*/
            var suc = await _context.Sucursal
                    .Include(c => c.StockItems)
                        .ThenInclude(ci => ci.Producto)
                .FirstOrDefaultAsync(m => m.SucursalId == sucursalid);
             var carrito = await _context.Carrito
                .Include(c => c.CarritosItems)
                    .ThenInclude(ci => ci.Producto)
                .FirstOrDefaultAsync(m => m.CarritoId == carritoid);
            List<CarritoItem> rej = new List<CarritoItem>();
            var str = "";
            foreach(CarritoItem i in carrito.CarritosItems){
                var it = suc.StockItems.Find(z => z.ProductoId == i.ProductoId);
                if(it !=null && it.Cantidad >= i.Cantidad){
                    it.Cantidad = it.Cantidad -i.Cantidad;
                }else{
                    str = (String.IsNullOrEmpty(str) ? i.Producto.Nombre : str +", "+ i.Producto.Nombre);
                    rej.Add(i);
                }
            }
            if(rej.Count() >= 1){
                 TempData["error"] = "En la sucursal "+suc.Nombre +" no hay stock suficiente de los siguientes productos: "+str+".";
                return RedirectToAction("SucursalCompra","Sucursales", new { id = carrito.CarritoId });
            }else{

                 _context.Sucursal.Update(suc);

                var compra = new Compra();
                compra.CompraId = Guid.NewGuid();
                compra.CarritoId = carritoid;
                compra.SucursalId = sucursalid;
                compra.Fecha = DateTime.Now;
                    
            
                    
                double tot = 0.00;
                foreach(CarritoItem i in carrito.CarritosItems)
                {
                    tot += (i.Cantidad* i.Producto.PrecioVigente);
                }
                compra.Total = tot;
                compra.ClienteId = carrito.ClienteId;
                _context.Compra.Add(compra);

                    
                
                carrito.Activo = false;
                _context.Carrito.Update(carrito);


                var carritoNuevo = new Carrito();
                carritoNuevo.CarritoId = Guid.NewGuid();
                carritoNuevo.ClienteId = carrito.ClienteId;
                carritoNuevo.Activo = true;
                carritoNuevo.Subtotal = 0;

                _context.Carrito.Add(carritoNuevo);

                await _context.SaveChangesAsync();
                return RedirectToAction("Details","Compras", new { id = compra.CompraId });
            }
            
            /*}
            ViewData["CarritoId"] = new SelectList(_context.Carrito, "CarritoId", "CarritoId", compra.CarritoId);
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Apellido", compra.ClienteId);
            ViewData["SucursalId"] = new SelectList(_context.Sucursal, "SucursalId", "Direccion", compra.SucursalId);
            return View(compra);*/
        }





        // GET: Compras/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compra.FindAsync(id);
            if (compra == null)
            {
                return NotFound();
            }
            ViewData["CarritoId"] = new SelectList(_context.Carrito, "CarritoId", "CarritoId", compra.CarritoId);
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Apellido", compra.ClienteId);
            ViewData["SucursalId"] = new SelectList(_context.Sucursal, "SucursalId", "Direccion", compra.SucursalId);
            return View(compra);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CompraId,ClienteId,CarritoId,Total,Fecha,SucursalId")] Compra compra)
        {
            if (id != compra.CompraId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraExists(compra.CompraId))
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
            ViewData["CarritoId"] = new SelectList(_context.Carrito, "CarritoId", "CarritoId", compra.CarritoId);
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Apellido", compra.ClienteId);
            ViewData["SucursalId"] = new SelectList(_context.Sucursal, "SucursalId", "Direccion", compra.SucursalId);
            return View(compra);
        }

        // GET: Compras/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compra
                .Include(c => c.Carrito)
                .Include(c => c.Cliente)
                .Include(c => c.Sucursal)
                .FirstOrDefaultAsync(m => m.CompraId == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var compra = await _context.Compra.FindAsync(id);
            _context.Compra.Remove(compra);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompraExists(Guid id)
        {
            return _context.Compra.Any(e => e.CompraId == id);
        }
    }
}

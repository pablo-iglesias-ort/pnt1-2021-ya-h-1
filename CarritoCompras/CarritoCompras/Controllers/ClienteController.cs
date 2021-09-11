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
    public class ClienteController : Controller
    {
        private CarritoComprasContext _context;
        static List<Cliente> clientes = new List<Cliente>()
        { 
            new Cliente
            {
                Id= Guid.NewGuid(),
                Nombre = "Lautaro",
                Email = "Lautaro@gmail.com",
                FechaAlta = new DateTime (2021,9,8),
                Apellido = "Gomez",
                Telefono = "12312312",
                Direccion= "Rioja 3221",
                Dni = "23444444"
            },
            new Cliente
            {
                Id= Guid.NewGuid(),
                Nombre = "Rama",
                Email = "Rama@gmail.com",
                FechaAlta = new DateTime (2021,9,8),
                Apellido = "Rodriguez",
                Telefono = "12312312",
                Direccion= "Boedo 3221",
                Dni = "23444222"
            },
            new Cliente
            {
                Id= Guid.NewGuid(),
                Nombre = "Santiago",
                Email = "Santiago@gmail.com",
                FechaAlta = new DateTime (2021,9,8),
                Apellido = "Idalgo",
                Telefono = "33333333",
                Direccion= "Lorenzo 3221",
                Dni = "30444444"
            },
        };


        public ClienteController(CarritoComprasContext context)
        {
            _context = context;
        }

        // GET: Cliente
        public async Task<IActionResult> Index()
        {
            return View(clientes);
        }

        // GET: Cliente/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = BuscarCliente(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Cliente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Dni,Apellido,Telefono,Direccion,Id,Nombre,Email,FechaAlta")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.Id = Guid.NewGuid();
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Cliente/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = BuscarCliente(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Dni,Apellido,Telefono,Direccion,Id,Nombre,Email,FechaAlta")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var clienteEncontrado = BuscarCliente(id);
                    clienteEncontrado.Apellido = cliente.Apellido;
                    clienteEncontrado.Nombre = cliente.Nombre;
                    clienteEncontrado.Direccion = cliente.Direccion;
                    clienteEncontrado.Telefono = cliente.Telefono;
                    clienteEncontrado.Email = cliente.Email;
                    clienteEncontrado.FechaAlta = cliente.FechaAlta;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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
            return View(cliente);
        }

        // GET: Cliente/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(Guid id)
        {
            return _context.Cliente.Any(e => e.Id == id);
        }
        private Cliente BuscarCliente(Guid? id)
        {


            var cliente = clientes.FirstOrDefault(u => u.Id == id);
            return cliente;
        }
    }
}

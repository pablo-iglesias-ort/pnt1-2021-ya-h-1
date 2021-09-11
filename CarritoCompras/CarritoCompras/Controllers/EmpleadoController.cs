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
    public class EmpleadoController : Controller
    {
        private CarritoComprasContext _context;
        static List<Empleado> empleados = new List<Empleado>()
        {
            new Empleado
            {
                Id= Guid.NewGuid(),
                Nombre = "Jorge",
                Email = "Jorge@gmail.com",
                FechaAlta = new DateTime (2021,9,8),
                Apellido = "Perez",
                Telefono = "7777777",
                Direccion= "Peron 3221",
            },
            new Empleado
            {
                Id= Guid.NewGuid(),
                Nombre = "Mateo",
                Email = "Mateo@gmail.com",
                FechaAlta = new DateTime (2021,9,8),
                Apellido = "Iri",
                Telefono = "454545454",
                Direccion= "Urquiza 3221",
            },
            new Empleado
            {
                Id= Guid.NewGuid(),
                Nombre = "Lorena",
                Email = "Lorena@gmail.com",
                FechaAlta = new DateTime (2021,9,8),
                Apellido = "Perez",
                Telefono = "4241423",
                Direccion= "San juan 3221",
            }
            };

        public EmpleadoController(CarritoComprasContext context)
        {
            _context = context;
        }

        // GET: Empleado
        public async Task<IActionResult> Index()
        {
            return View(empleados);
        }

        // GET: Empleado/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = BuscarEmpleado(id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleado/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empleado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Apellido,Telefono,Direccion,Id,Nombre,Email,FechaAlta")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                empleado.Id = Guid.NewGuid();
                _context.Add(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empleado);
        }

        // GET: Empleado/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = BuscarEmpleado(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // POST: Empleado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Apellido,Telefono,Direccion,Id,Nombre,Email,FechaAlta")] Empleado empleado)
        {
            if (id != empleado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var empleadoEncontrado = BuscarEmpleado(id);
                    empleadoEncontrado.Apellido = empleado.Apellido;
                    empleadoEncontrado.Nombre = empleado.Nombre;
                    empleadoEncontrado.Direccion = empleado.Direccion;
                    empleadoEncontrado.Telefono = empleado.Telefono;
                    empleadoEncontrado.Email = empleado.Email;
                    empleadoEncontrado.FechaAlta = empleado.FechaAlta;
                }
                catch (Exception)
                {
                    if (!EmpleadoExists(empleado.Id))
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
            return View(empleado);
        }

        // GET: Empleado/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = BuscarEmpleado(id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var empleado = await _context.Empleado.FindAsync(id);
            _context.Empleado.Remove(empleado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(Guid id)
        {
            return _context.Empleado.Any(e => e.Id == id);
        }
        private Empleado BuscarEmpleado(Guid? id)
        {


            var empleado = empleados.FirstOrDefault(u => u.Id == id);
            return empleado;
        }
    }
}

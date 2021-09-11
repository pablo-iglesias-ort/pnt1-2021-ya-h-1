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
    public class UsuarioController : Controller
    {
        private CarritoComprasContext _context;
        static List<Usuario> usuarios = new List<Usuario>()
        {
            new Usuario
            {
                Id = Guid.NewGuid(),
                Nombre = "usuario1",
                Email = "usuario1@gmail.com",
                FechaAlta = new DateTime (2021,9,8),
                Password = "1234"
            },
            new Usuario
            {
                Id = Guid.NewGuid(),
                Nombre = "usuario2",
                Email = "usuario2@gmail.com",
                FechaAlta = new DateTime (2021,9,6),
                Password = "1234"
            },
            new Usuario
            {
                Id = Guid.NewGuid(),
                Nombre = "usuario3",
                Email = "usuario3@gmail.com",
                FechaAlta = new DateTime (2021,9,4),
                Password = "1234"
            }
        };

        public UsuarioController(CarritoComprasContext context)
        {
            _context = context;
        }

        // GET: Usuario
        public async Task<IActionResult> Index()
        {
            return View(usuarios);
        }

        // GET: Usuario/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = BuscarUsuario(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Password,Id,Nombre,Email,FechaAlta")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Id = Guid.NewGuid();
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = BuscarUsuario(id); ;
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Password,Id,Nombre,Email,FechaAlta")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioEncontrado = BuscarUsuario(id);
                    usuarioEncontrado.Password = usuario.Password;
                    usuarioEncontrado.Nombre = usuario.Nombre;
                    usuarioEncontrado.Email = usuario.Email;
                    usuarioEncontrado.FechaAlta = usuario.FechaAlta;

                }
                catch (Exception)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(Guid id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }
        private Usuario BuscarUsuario(Guid? id)
        {
            
           
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            return usuario;
        }
    }
}

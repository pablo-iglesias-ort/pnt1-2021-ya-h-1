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
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace CarritoCompras.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly CarritoComprasContext _context;        
        private readonly ISeguridad seguridad = new Seguridad();

        public UsuariosController(CarritoComprasContext context)
        {
            _context = context;
        }

        [Authorize(Roles = nameof(Rol.Administrador))]
        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuario.ToListAsync());
        }

        // GET: Usuarios/Details/5
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Details(Guid? id)
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

        // GET: Usuarios/Create
        [Authorize(Roles = nameof(Rol.Administrador))]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Create([Bind("Id,UserName,Nombre,Apellido,Telefono,Direccion,FechaAlta,Password")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {                                
                empleado.Id = Guid.NewGuid();
                empleado.FechaAlta = DateTime.Now.Date;                
                _context.Add(empleado);             
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empleado);
        }

        // GET: Usuarios/Edit/5
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserName,Nombre,Apellido,Telefono,Direccion,FechaAlta,Password")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
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
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        [Authorize(Roles = nameof(Rol.Administrador))]
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

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Administrador))]
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

        // GET: Usuario
        [AllowAnonymous]
        public IActionResult Ingresar(string returnUrl)
        {
            TempData["UrlIngreso"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Ingresar(string NombreUsuario, string Password)
        {
            // Guardamos la URL a la que debemos redirigir al usuario
            var urlIngreso = TempData["UrlIngreso"] as string;

            // Verificamos que ambos esten informados
            if (!string.IsNullOrEmpty(NombreUsuario) && !string.IsNullOrEmpty(Password))
            {

                // Verificamos que exista el usuario
                var user = await _context.Usuario.FirstOrDefaultAsync(u => u.UserName == NombreUsuario);
                if (user != null)
                {

                    // Verificamos que coincida la contraseña
                    var contraseña = seguridad.EncriptarPass(Password);
                    if (contraseña.SequenceEqual(user.Password))
                    {
                        // Creamos los Claims (credencial de acceso con informacion del usuario)-- cookies
                        ClaimsIdentity identidad = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                        // Agregamos a la credencial el nombre de usuario
                        identidad.AddClaim(new Claim(ClaimTypes.Name, NombreUsuario));
                        // Agregamos a la credencial el nombre del estudiante/administrador
                        identidad.AddClaim(new Claim(ClaimTypes.GivenName, user.Nombre));
                        // Agregamos a la credencial el Rol
                        identidad.AddClaim(new Claim(ClaimTypes.Role, user.Rol.ToString()));
                        // Agregamos el Id de Usuario
                        identidad.AddClaim(new Claim("IdDeUsuario", user.Id.ToString()));
                        identidad.AddClaim(new Claim("NombreUsuario", user.Nombre.ToString()));


                        ClaimsPrincipal principal = new ClaimsPrincipal(identidad);

                        // Ejecutamos el Login
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        if (!string.IsNullOrEmpty(urlIngreso))
                        {
                            return Redirect(urlIngreso);
                        }
                        else
                        {
                            // Redirigimos a la pagina principal
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }

            ViewBag.ErrorEnLogin = "Verifique el usuario y contraseña";
            TempData["UrlIngreso"] = urlIngreso; // Volvemos a enviarla en el TempData para no perderla
            return View();

        }

        [Authorize]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccesoDenegado()
        {
            return View();
        }


        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Registrarse(Cliente cliente, string pass, string returnUrl)
        {
            var usuarioViejo = _context.Cliente.FirstOrDefaultAsync(n => n.UserName == cliente.UserName);
            TempData["UrlIngreso"] = returnUrl;

            if (usuarioViejo.Result == null)
            {
                if (ModelState.IsValid)
                {                    
                    var nuevoCliente = new Cliente();
                    nuevoCliente.Id = Guid.NewGuid();
                    nuevoCliente.Nombre = cliente.Nombre;
                    nuevoCliente.Apellido = cliente.Apellido;
                    nuevoCliente.Telefono = cliente.Telefono;
                    nuevoCliente.Direccion = cliente.Direccion;                
                    nuevoCliente.UserName = cliente.UserName;
                    nuevoCliente.Password = seguridad.EncriptarPass(pass); 
                    nuevoCliente.DNI = cliente.DNI;
                    nuevoCliente.FechaAlta = DateTime.Now;
                    _context.Cliente.Add(nuevoCliente);

                    var carrito = new Carrito
                    {
                        CarritoId = Guid.NewGuid(),
                        ClienteId = nuevoCliente.Id,
                        Activo = true
                    };
                    _context.Carrito.Add(carrito);

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Ingresar));            
                }
            }
            else
            {                
                ViewBag.Error = "El nombre de usuario "+cliente.UserName+ " ya existe. Ingrese uno distinto.";
                return View(cliente);
            }
            return View(cliente);
        }
    }
}

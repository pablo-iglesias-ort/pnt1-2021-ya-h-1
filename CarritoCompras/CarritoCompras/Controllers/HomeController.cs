using CarritoCompras.Data;
using CarritoCompras.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace CarritoCompras.Controllers
{
    public class HomeController : Controller
    {
        private readonly CarritoComprasContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, CarritoComprasContext context)
        {
            _logger = logger;
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var categorias = _context.Categoria
                                             .Select(c => new Categoria
                                             {
                                                 Nombre = c.Nombre,
                                                 Productos = c.Productos.Where(p => p.Activo).ToList()
                                             });
            return View(categorias);
        }

        [AllowAnonymous]
        public IActionResult Creditos()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

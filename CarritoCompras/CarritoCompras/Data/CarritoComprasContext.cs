using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarritoCompras.Models;

namespace CarritoCompras.Data
{
    public class CarritoComprasContext : DbContext
    {
        public CarritoComprasContext (DbContextOptions<CarritoComprasContext> options)
            : base(options)
        {
        }

        public DbSet<CarritoCompras.Models.Producto> Producto { get; set; }

<<<<<<< HEAD
=======

>>>>>>> f953aa4d2b4ce7d01151ed4f5a2fd2ffff8d01da
        public DbSet<CarritoCompras.Models.Carrito> Carrito { get; set; }

        public DbSet<CarritoCompras.Models.Sucursal> Sucursal { get; set; }

        public DbSet<CarritoCompras.Models.StockItem> StockItem { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarritoCompras.Models;
using Microsoft.AspNetCore.Identity;

namespace CarritoCompras.Data
{
    public class CarritoComprasContext : DbContext
    {
        public CarritoComprasContext (DbContextOptions<CarritoComprasContext> options)
            : base(options)
        {
        }

        
        public DbSet<CarritoCompras.Models.Producto> Producto { get; set; }

        public DbSet<CarritoCompras.Models.Empleado> Empleado { get; set; }

        public DbSet<CarritoCompras.Models.Carrito> Carrito { get; set; }

        public DbSet<CarritoCompras.Models.Sucursal> Sucursal { get; set; }

        public DbSet<CarritoCompras.Models.StockItem> StockItem { get; set; }

        public DbSet<CarritoCompras.Models.CarritoItem> CarritoItem { get; set; }

        public DbSet<CarritoCompras.Models.Usuario> Usuario { get; set; }

        public DbSet<CarritoCompras.Models.Cliente> Cliente { get; set; }

        public DbSet<CarritoCompras.Models.Categoria> Categoria { get; set; }

        public DbSet<CarritoCompras.Models.Compra> Compra { get; set; }

    }
}

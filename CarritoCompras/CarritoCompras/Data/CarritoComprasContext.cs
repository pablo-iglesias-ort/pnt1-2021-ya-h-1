﻿using System;
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

        public DbSet<CarritoCompras.Models.Carrito> Carrito { get; set; }

        public DbSet<CarritoCompras.Models.Sucursal> Sucursal { get; set; }

        public DbSet<CarritoCompras.Models.StockItem> StockItem { get; set; }

        public DbSet<CarritoCompras.Models.CarritoItem> CarritoItem { get; set; }

    }
}

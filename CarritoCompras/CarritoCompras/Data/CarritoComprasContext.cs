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
    }
}

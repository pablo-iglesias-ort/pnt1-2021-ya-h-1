using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public class StockItem
    {
        public Guid Id { get; set; }
        public Sucursal sucursal { get; set; }
        public Producto producto { get; set; }
        public int cantidad { get; set; }

    }
}

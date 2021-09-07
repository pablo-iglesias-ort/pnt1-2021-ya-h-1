using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public class StockItem
    {
        public Guid Id { get; set; }
        public Sucursal Sucursal { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }

    }
}

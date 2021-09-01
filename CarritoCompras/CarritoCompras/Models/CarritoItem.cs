using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public class CarritoItem
    {
        public Carrito Carrito { get; set; }
        public Producto Producto { get; set; }
        public double ValorUnitario { get; set; }
        public int Cantidad { get; set; }
        public double Subtotal { get; set; }
    }
}

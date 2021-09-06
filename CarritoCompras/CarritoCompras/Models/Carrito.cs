using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public class Carrito
    {
        public Guid Id { get; set; }
        public bool Activo { get; set; }
        public Cliente Cliente { get; set; }
        public List<CarritoItem> CarritosItems { get; set; }
        public double Subtotal { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public class CarritoItem
    {
        [Required]
        public Guid Id { get; set; }
        public Carrito Carrito { get; set; }
        public Producto Producto { get; set; }
        [Required]
        public double ValorUnitario { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public double Subtotal { get; set; }
    }
}

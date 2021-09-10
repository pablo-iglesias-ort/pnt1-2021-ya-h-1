using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public class Carrito
    {
        [Required]
        public Guid Id { get; set; }
        public bool Activo { get; set; }
        public Cliente Cliente { get; set; }
        public List<CarritoItem> CarritosItems { get; set; }

        [Required]
        public double Subtotal { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public class CarritoItem
    {
        private const string completarObligatorio = "Falta completar {0} , Obligatorio";
        [Required(ErrorMessage = completarObligatorio)]

        public Guid CarritoItemId { get; set; }

        [Required(ErrorMessage = completarObligatorio)]
        public Guid CarritoId { get; set; }
        public Carrito Carrito { get; set; }

        [Required(ErrorMessage = completarObligatorio)]
        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; }

        [Required(ErrorMessage = completarObligatorio)]
        public Double ValorUnitario { get; set; }

        [Required(ErrorMessage = completarObligatorio)]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = completarObligatorio)]
        public Double Subtotal { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public class Carrito
    {
        private const string completarObligatorio = "Falta completar {0} , Obligatorio";
        [Required(ErrorMessage = completarObligatorio)]
        public Guid CarritoId { get; set; }
        [Required(ErrorMessage = completarObligatorio)]
        public Guid ClienteId { get; set; }
        [Required(ErrorMessage = completarObligatorio)]
        public bool Activo { get; set; }
        [Required(ErrorMessage = completarObligatorio)]
        public Cliente Cliente { get; set; }
        public List<CarritoItem> CarritosItems { get; set; }

        [Required(ErrorMessage = completarObligatorio)]
        public double Subtotal { get; set; }
    }
}

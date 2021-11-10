using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Range(1, int.MaxValue, ErrorMessage = "Ingrese 1 o más productos")]
        public int Cantidad { get; set; }
        
    }
}

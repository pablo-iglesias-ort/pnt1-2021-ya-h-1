using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public class Compra
    {
        private const string completarObligatorio = "Falta completar {0} , Obligatorio";
        public Guid CompraId { get; set; }

        [Required(ErrorMessage = completarObligatorio)]
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [Required(ErrorMessage = completarObligatorio)]
        public Guid CarritoId { get; set; }
        public Carrito Carrito { get; set; }

        [Required(ErrorMessage = completarObligatorio)]
        
        public Double Total { get; set; }

        public DateTime Fecha { get; set; }

        public Guid SucursalId { get; set; }
        public Sucursal Sucursal { get; set; }
    }
}

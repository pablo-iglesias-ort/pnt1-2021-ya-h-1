using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public class StockItem
    {
        private const string completarObligatorio = "Falta completar {0} , Obligatorio";

        public Guid StockItemId { get; set; }

        [Required(ErrorMessage = completarObligatorio)]
        public Guid SucursalId { get; set; }
        public Sucursal Sucursal { get; set; }

        [Required(ErrorMessage = completarObligatorio)]
        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; }

        [Required(ErrorMessage = completarObligatorio)]
        public int Cantidad { get; set; }

    }
}

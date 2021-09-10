using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public class StockItem
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Sucursal Sucursal { get; set; }
        [Required]
        public Producto Producto { get; set; }
        [Required]
        public int Cantidad { get; set; }

    }
}

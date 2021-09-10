using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public class Sucursal
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public List<StockItem> StockItems { get; set; }

    }
}

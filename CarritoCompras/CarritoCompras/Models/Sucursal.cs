using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public class Sucursal
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public List<StockItem> StockItems { get; set; }

    }
}

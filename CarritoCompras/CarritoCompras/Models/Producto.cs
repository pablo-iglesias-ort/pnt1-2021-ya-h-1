using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public class Producto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double PrecioVigente { get; set; }
        public bool Activo { get; set; }
        public Categoria Categoria { get; set; }
        /*public int Stock { get; set; }
        public string CodigoProducto { get; set; }
        public double DescuentoProducto { get; set; }
        public List<string> Tags { get; set; }
        */
        public Producto()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public class Categoria
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<Producto> Productos { get; set; }

        //public bool Habilitado { get; set; }
        //public Categoria CategoriaPadre { get; set; }
        //public double DescuentoCategoria { get; set; }
        public Categoria()
        {

        }
    }
}

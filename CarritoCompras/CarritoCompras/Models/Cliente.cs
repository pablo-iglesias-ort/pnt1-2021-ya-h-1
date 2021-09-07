using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public class Cliente : Operador
    {
        public string Dni { get; set; }

        //public List<Compra> Compras { get; set; }
        public List<Carrito> Carritos { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public class Cliente : Empleado
    {
        [Required(ErrorMessage = "Por favor complete el campo Dni")]

        public string Dni { get; set; }
        
        public List<Compra> Compras { get; set; }
        public List<Carrito> Carritos { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public class Cliente : Usuario
    {
        [Required(ErrorMessage = "Por favor complete el campo Dni")]
        [RegularExpression(@"[0-9]{2}[0-9]{3}[0-9]{3}", ErrorMessage = "El dni debe tener 8 caracteres numericos")]
        public string DNI { get; set; }
        public Nullable<Guid> CarritoId { get; set; }
        public List<Compra> Compras { get; set; }
        public Carrito Carrito { get; set; }
        

    }
}

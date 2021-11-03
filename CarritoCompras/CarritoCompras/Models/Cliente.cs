using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public class Cliente : Usuario
    {
        // Atributos de clase
        [Required(ErrorMessage = "Por favor complete el campo Dni")]
        [RegularExpression(@"[0-9]{2}[0-9]{3}[0-9]{3}", ErrorMessage = "El dni debe tener 8 caracteres numericos")]
        public string DNI { get; set; }

        //Relaciones con otras tablas
        
        public List<Carrito> Carritos { get; set; }

        public List<Compra> Compras { get; set; }        

        public override Rol Rol => Rol.Cliente;

        public Cliente()
        {
            Carritos = new List<Carrito>();
            Compras = new List<Compra>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public class Empleado : Persona
    {
        [Required(ErrorMessage = "Por favor complete el Apellido")]

        public string Apellido { get; set; }
        [Required(ErrorMessage = "Por favor complete el Telefono")]

        public string Telefono { get; set; }
        [Required(ErrorMessage = "Por favor complete el Telefono")]

        public string Direccion { get; set; }
    }
}

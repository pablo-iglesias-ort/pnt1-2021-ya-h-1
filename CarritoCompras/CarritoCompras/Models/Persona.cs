using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public abstract class Persona
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Por favor complete el campo Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Por favor complete el campo Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor complete el campo FechaAlta")]

        public DateTime FechaAlta { get; set; }
    }
}

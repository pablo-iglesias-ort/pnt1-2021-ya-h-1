using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public class Usuario : Persona
    {
        [Required(ErrorMessage ="Password: Tiene que contener entre 4 y 8 Caracteres")]
        public string Password { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public abstract class Usuario


    {
        private const string completarObligatorio = "Falta completar {0} , Obligatorio";

        [Key]
        public Guid Id { get; set; }


        [Required(ErrorMessage = completarObligatorio)]
        [StringLength(120, ErrorMessage = "{0} admite como maximo {1} digitos")]
        public string UserName { get; set; }

        [Required(ErrorMessage = completarObligatorio)]
        [StringLength(120, ErrorMessage = "{0} admite como maximo {1} digitos")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = completarObligatorio)]
        [StringLength(120, ErrorMessage = "{0} admite como maximo {1} digitos")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = completarObligatorio)]

        [StringLength(20, ErrorMessage = "{0} admite como maximo {1} digitos")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = completarObligatorio)]
        [StringLength(120, ErrorMessage = "{0} admite como maximo {1} digitos")]
        public string Direccion { get; set; }


        [Required(ErrorMessage = completarObligatorio)]
        [DataType(DataType.Date)]        
        public DateTime FechaAlta { get; set; }
                        
        [Display(Name = "Contraseña")]
        public byte[] Password { get; set; }

        [Required]
        public abstract Rol Rol { get; }

    }
}

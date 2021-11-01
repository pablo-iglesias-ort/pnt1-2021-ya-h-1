using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public class Sucursal
    {
        private const string completarObligatorio = "Falta completar {0} , Obligatorio";

        public Guid SucursalId { get; set; }

        [Required(ErrorMessage = completarObligatorio)]
        [MaxLength(120, ErrorMessage = "El {0} soporta un maximo de {1} caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = completarObligatorio)]
        [MaxLength(120, ErrorMessage = "El {0} soporta un maximo de {1} caracteres")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = completarObligatorio)]
        [RegularExpression(@"^[0-9]{1,20}$", ErrorMessage = "El {0} solo registra numeros")]
        [MaxLength(20, ErrorMessage = "El {0} admite como maximo {1} digitos")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = completarObligatorio)]
        public string Email { get; set; }

        public List<StockItem> StockItems { get; set; }

    }
}

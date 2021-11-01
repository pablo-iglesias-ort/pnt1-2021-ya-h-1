using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public class Producto
    {
        private const string completarObligatorio = "Falta completar {0} , Obligatorio";

        public Guid ProductoId { get; set; }

        [Required(ErrorMessage = completarObligatorio)]
        [StringLength(25, ErrorMessage = "El {0} tiene un limite de {1} caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = completarObligatorio)]
        [MaxLength(200, ErrorMessage = "El {0} tiene un limite de {1} caracteres.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = completarObligatorio)]
        public float PrecioVigente { get; set; }

        [Required(ErrorMessage = completarObligatorio)]
        public Boolean Activo { get; set; }

        [Required(ErrorMessage = completarObligatorio)]
        public Guid CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        [Required(ErrorMessage = completarObligatorio)]
        public string Foto { get; set; } = "default.png";
    }
}

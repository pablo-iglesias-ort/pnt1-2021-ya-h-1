using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public abstract class Operador : Persona
    {
        public string Apellido { get; set }
        public string Telefono { get; set }
        public string Direccion { get; set }
    }
}

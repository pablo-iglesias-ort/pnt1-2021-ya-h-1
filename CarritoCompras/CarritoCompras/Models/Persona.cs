using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public abstract class Persona
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }

        public DateTime FechaAlta { get; set; }
    }
}

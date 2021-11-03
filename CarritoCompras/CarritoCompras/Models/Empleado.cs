using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public class Empleado : Usuario
    {
        public override Rol Rol => Rol.Administrador;
    }
}

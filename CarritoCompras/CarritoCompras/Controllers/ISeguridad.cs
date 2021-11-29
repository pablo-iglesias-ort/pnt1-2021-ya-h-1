using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Controllers
{
    public interface ISeguridad
    {
        public byte[] EncriptarPass(string pass);        
        public byte[] EncriptarPassFromByteArray(byte[] pass);
        public bool ValidarPass(string pass);
    }
}

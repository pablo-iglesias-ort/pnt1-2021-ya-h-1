using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarritoCompras.Models
{
    public class Compra
    {
        public Guid Id { get; set; }
        public Cliente Cliente { get; set; }
        public Carrito Carrito { get; set; }
        public double Total { get; set; }
        /*public Usuario Usuario { get; set; }
        public string MetodoDePago { get; set; }
        public List<CarritoItem> ProductosAdquiridos { get; set; }
        public DateTime TimeStamp { get; set; } //Fecha y Hora
        public double DescuentoAplicado { get; set; }
        public DateTime FechaEntregaEstimada { get; set; }
        */

        public Compra()
        {

        }
    }
}

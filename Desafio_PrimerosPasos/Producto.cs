using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_PrimerosPasos
{
    public class Producto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public double Costo { get; set; }
        public double PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int IdUsuario { get; set; }

        public Producto(int Id, string Descripcion, double Costo, double PrecioVenta, int Stock, int IdUsuario)
        {
            this.Id = Id;
            this.Descripcion = Descripcion;
            this.Costo = Costo;
            this.PrecioVenta = PrecioVenta;
            this.Stock = Stock;
            this.IdUsuario = IdUsuario;
        }
    }
}

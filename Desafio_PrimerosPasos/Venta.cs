using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_PrimerosPasos
{
    public class Venta
    {
        public int Id { get; set; }
        public string Comentarios { get; set; }

        public Venta(int Id, string Comentarios)
        { 
            this.Id = Id;
            this.Comentarios = Comentarios;
        }
    }
}
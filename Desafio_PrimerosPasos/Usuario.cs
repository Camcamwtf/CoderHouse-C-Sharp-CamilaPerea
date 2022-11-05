using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_PrimerosPasos
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasenia { get; set; }
        public string Mail { get; set; }

        public Usuario(int Id, string Nombre, string Apellido, string NombreUsuario, string Contrasenia, string Mail)
        {
            this.Id = Id;
            this.Nombre = Nombre;
            this.Apellido = Apellido;
            this.NombreUsuario = NombreUsuario;
            this.Contrasenia = Contrasenia;
            this.Mail = Mail;
        }
    }
}
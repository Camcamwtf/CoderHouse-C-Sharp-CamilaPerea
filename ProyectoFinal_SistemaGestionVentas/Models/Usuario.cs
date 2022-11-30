namespace ProyectoFinal_SistemaGestionVentas.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Contrasenia { get; set; }
        public string? Mail { get; set; }

        public Usuario() 
        {
            Id = 0;
            Nombre = string.Empty;
            Apellido = string.Empty;
            NombreUsuario = string.Empty;
            Contrasenia = string.Empty;
            Mail = string.Empty;
        }
    }
}
using ProyectoFinal_SistemaGestionVentas.Models;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinal_SistemaGestionVentas.Repositories
{
    public class InicioDeSesionRepository
    {
        private SqlConnection? connection;
        private string? connectionString = "Server=sql.bsite.net\\MSSQL2016;" +
            "Database=camcamwtf_sistema_gestion_csharp;" +
            "User Id=camcamwtf_sistema_gestion_csharp;" +
            "Password=072032";

        public InicioDeSesionRepository()
        {
            try 
            { 
                connection = new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool VerificarUsuario(Usuario usuario)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida.");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario WHERE NombreUsuario = @NombreUsuario AND Contrasenia = @Contrasenia", connection))
                {
                    connection.Open();
                    cmd.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario});
                    cmd.Parameters.Add(new SqlParameter("Contrasenia", SqlDbType.VarChar) { Value = usuario.Contrasenia });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
            catch
            {
                throw;
            }
            finally 
            { 
                connection.Close(); 
            }
        }
    }
}
using System.Data.SqlClient;
using ProyectoFinal_SistemaGestionVentas.Models;

namespace ProyectoFinal_SistemaGestionVentas.Repositories
{
    public class VentaRepository
    {
        private SqlConnection? connection;
        private string? connectionString = "Server=sql.bsite.net\\MSSQL2016;" +
            "Database=camcamwtf_sistema_gestion_csharp;" +
            "User Id=camcamwtf_sistema_gestion_csharp;" +
            "Password=072032";

        public VentaRepository()
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

        public List<Venta> listarVentas()
        {
            List<Venta> lista = new List<Venta>();

            if (connection == null)
            {
                throw new Exception("Conexión no establecida.");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Venta", connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Venta venta = new Venta();
                            venta.Id = Convert.ToInt32(reader["Id"].ToString());
                            venta.Comentarios = reader["Comentarios"].ToString();
                            venta.IdUsuario = Convert.ToInt32(reader["IdUsuario"].ToString());
                            lista.Add(venta);
                        }
                    }
                }
                connection.Close();
            }
            catch
            {
                throw;
            }
            return lista;
        }
    }
}
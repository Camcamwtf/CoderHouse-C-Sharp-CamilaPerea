using System.Data.SqlClient;
using ProyectoFinal_SistemaGestionVentas.Models;

namespace ProyectoFinal_SistemaGestionVentas.Repositories
{
    public class ProductoRepository
    {
        private SqlConnection? connection;
        private string? connectionString = "Server=sql.bsite.net\\MSSQL2016;" +
            "Database=camcamwtf_sistema_gestion_csharp;" +
            "User Id=camcamwtf_sistema_gestion_csharp;" +
            "Password=072032";

        public ProductoRepository()
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

        public List<Producto> listarProductos()
        {
            List<Producto> lista = new List<Producto>();

            if (connection == null)
            {
                throw new Exception("Conexión no establecida.");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Producto", connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Producto producto = new Producto();
                            producto.Id = Convert.ToInt32(reader["Id"].ToString());
                            producto.Descripcion = reader["Descripcion"].ToString();
                            producto.Costo = Convert.ToDouble(reader["Costo"].ToString());
                            producto.PrecioVenta = Convert.ToDouble(reader["PrecioVenta"].ToString());
                            producto.Stock = Convert.ToInt32(reader["Stock"].ToString());
                            producto.IdUsuario = Convert.ToInt32(reader["IdUsuario"].ToString());
                            lista.Add(producto);
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
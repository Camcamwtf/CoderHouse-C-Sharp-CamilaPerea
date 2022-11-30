using System.Data.SqlClient;
using ProyectoFinal_SistemaGestionVentas.Models;

namespace ProyectoFinal_SistemaGestionVentas.Repositories
{
    public class ProductoVendidoRepository
    {
        private SqlConnection? connection;
        private string? connectionString = "Server=sql.bsite.net\\MSSQL2016;" +
            "Database=camcamwtf_sistema_gestion_csharp;" +
            "User Id=camcamwtf_sistema_gestion_csharp;" +
            "Password=072032";

        public ProductoVendidoRepository()
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

        public List<ProductoVendido> listarProductosVendidos()
        {
            List<ProductoVendido> lista = new List<ProductoVendido>();

            if (connection == null)
            {
                throw new Exception("Conexión no establecida.");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM ProductoVendido", connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductoVendido productoVendido = new ProductoVendido();
                            productoVendido.Id = Convert.ToInt32(reader["Id"].ToString());
                            productoVendido.IdProducto = Convert.ToInt32(reader["IdProducto"].ToString());
                            productoVendido.IdVenta = Convert.ToInt32(reader["IdVenta"].ToString());
                            productoVendido.Stock = Convert.ToInt32(reader["Stock"].ToString());
                            lista.Add(productoVendido);
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
using System.Data;
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

        public List<ProductoVendido> ListarProductosVendidos()
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
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ProductoVendido productoVendido = ObtenerProductoVendidoDesdeReader(reader);
                                lista.Add(productoVendido);
                            }
                        }
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
            return lista;
        }

        public ProductoVendido? ObtenerProductoVendido(int Id)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida.");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM ProductoVendido WHERE Id = @Id", connection))
                {
                    connection.Open();
                    cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt) { Value = Id });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            ProductoVendido productoVendido = ObtenerProductoVendidoDesdeReader(reader);
                            return productoVendido;
                        }
                        else
                        {
                            return null;
                        }
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

        public ProductoVendido? CrearProductoVendido(ProductoVendido productoVendido)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida.");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO ProductoVendido(Stock, IdProducto, IdVenta) VALUES(@Stock, @IdProducto, @IdVenta); SELECT @@Identity ", connection))
                {
                    connection.Open();
                    cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int) { Value = productoVendido.Stock });
                    cmd.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.BigInt) { Value = productoVendido.IdProducto });
                    cmd.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.BigInt) { Value = productoVendido.IdVenta });
                    productoVendido.Id = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    return productoVendido;
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

        public ProductoVendido? ModificarProductoVendido(int Id, ProductoVendido productoVendidoParaActualizar)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida.");
            }
            try
            {
                ProductoVendido? productoVendido = ObtenerProductoVendido(Id);
                if (productoVendido == null)
                {
                    return null;
                }

                List<string> camposParaActualizar = new List<string>();
                if (productoVendido.Stock != productoVendidoParaActualizar.Stock && productoVendidoParaActualizar.Stock > 0)
                {
                    camposParaActualizar.Add("Stock = @Stock");
                    productoVendido.Stock = productoVendidoParaActualizar.Stock;
                }
                if (camposParaActualizar.Count == 0)
                {
                    throw new Exception("No New Fields To Update.");
                }

                using (SqlCommand cmd = new SqlCommand($"UPDATE ProductoVendido SET {String.Join(", ", camposParaActualizar)} WHERE Id = @Id", connection))
                {
                    cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int) { Value = productoVendido.Stock });
                    cmd.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.BigInt) { Value = productoVendido.IdProducto });
                    cmd.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.BigInt) { Value = productoVendido.IdVenta });
                    cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt) { Value = Id });
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return productoVendido;
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

        public bool EliminarProductoVendido(int Id)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida.");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand cmd = new SqlCommand("DELETE FROM ProductoVendido WHERE Id = @Id", connection))
                {
                    connection.Open();
                    cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt) { Value = Id });
                    filasAfectadas = cmd.ExecuteNonQuery();
                }
                connection.Close();
                return filasAfectadas > 0;
            }
            catch
            {
                throw;
            }
        }

        public static ProductoVendido ObtenerProductoVendidoDesdeReader(SqlDataReader reader)
        {
            ProductoVendido productoVendido = new ProductoVendido();
            productoVendido.Id = Convert.ToInt32(reader["Id"].ToString());
            productoVendido.Stock = Convert.ToInt32(reader["Stock"].ToString());
            productoVendido.IdProducto = Convert.ToInt32(reader["IdProducto"].ToString());
            productoVendido.IdVenta = Convert.ToInt32(reader["IdVenta"].ToString());
            return productoVendido;
        }
    }
}
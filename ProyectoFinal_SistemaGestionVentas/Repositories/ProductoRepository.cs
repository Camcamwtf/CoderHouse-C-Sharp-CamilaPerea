using System.Data;
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

        public List<Producto> ListarProductos()
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
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Producto producto = ObtenerProductoDesdeReader(reader);
                                lista.Add(producto);
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

        public Producto? ObtenerProducto(int Id)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida.");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Producto WHERE Id = @Id", connection))
                {
                    connection.Open();
                    cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt) { Value = Id });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Producto producto = ObtenerProductoDesdeReader(reader);
                            return producto;
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

        public Producto? CrearProducto(Producto producto)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida.");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Producto(Descripcion, Costo, PrecioVenta, Stock, IdUsuario) VALUES(@Descripcion, @Costo, @PrecioVenta, @Stock, @IdUsuario); SELECT @@Identity ", connection))
                {
                    connection.Open();
                    cmd.Parameters.Add(new SqlParameter("Descripcion", SqlDbType.VarChar) { Value = producto.Descripcion });
                    cmd.Parameters.Add(new SqlParameter("Costo", SqlDbType.Money) { Value = producto.Costo });
                    cmd.Parameters.Add(new SqlParameter("PrecioVenta", SqlDbType.Money) { Value = producto.PrecioVenta });
                    cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int) { Value = producto.Stock });
                    cmd.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = producto.IdUsuario });
                    producto.Id = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    return producto;
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

        public Producto? ModificarProducto(int Id, Producto productoParaActualizar)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida.");
            }
            try
            {
                Producto? producto = ObtenerProducto(Id);
                if (producto == null)
                {
                    return null;
                }

                List<string> camposParaActualizar = new List<string>();
                if (producto.Descripcion != productoParaActualizar.Descripcion && !string.IsNullOrEmpty(productoParaActualizar.Descripcion))
                {
                    camposParaActualizar.Add("Descripcion = @Descripcion");
                    producto.Descripcion = productoParaActualizar.Descripcion;
                }
                if (producto.Costo != productoParaActualizar.Costo && productoParaActualizar.Costo > 0)
                {
                    camposParaActualizar.Add("Costo = @Costo");
                    producto.Costo = productoParaActualizar.Costo;
                }
                if (producto.PrecioVenta != productoParaActualizar.PrecioVenta && productoParaActualizar.PrecioVenta > 0)
                {
                    camposParaActualizar.Add("PrecioVenta = @PrecioVenta");
                    producto.PrecioVenta = productoParaActualizar.PrecioVenta;
                }
                if (producto.Stock != productoParaActualizar.Stock && productoParaActualizar.Stock > 0)
                {
                    camposParaActualizar.Add("Stock = @Stock");
                    producto.Stock = productoParaActualizar.Stock;
                }
                if (camposParaActualizar.Count == 0)
                {
                    throw new Exception("No New Fields To Update.");
                }

                using (SqlCommand cmd = new SqlCommand($"UPDATE Producto SET {String.Join(", ", camposParaActualizar)} WHERE Id = @Id", connection))
                {
                    cmd.Parameters.Add(new SqlParameter("Descripcion", SqlDbType.VarChar) { Value = producto.Descripcion });
                    cmd.Parameters.Add(new SqlParameter("Costo", SqlDbType.Money) { Value = producto.Costo });
                    cmd.Parameters.Add(new SqlParameter("PrecioVenta", SqlDbType.Money) { Value = producto.PrecioVenta });
                    cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int) { Value = producto.Stock });
                    cmd.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = producto.IdUsuario });
                    cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt) { Value = Id });
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return producto;
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

        public bool EliminarProducto(int Id)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida.");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Producto WHERE Id = @Id", connection))
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

        private static Producto ObtenerProductoDesdeReader(SqlDataReader reader)
        {
            Producto producto = new Producto();
            producto.Id = Convert.ToInt32(reader["Id"].ToString());
            producto.Descripcion = reader["Descripcion"].ToString();
            producto.Costo = Convert.ToDouble(reader["Costo"].ToString());
            producto.PrecioVenta = Convert.ToDouble(reader["PrecioVenta"].ToString());
            producto.Stock = Convert.ToInt32(reader["Stock"].ToString());
            producto.IdUsuario = Convert.ToInt32(reader["IdUsuario"].ToString());
            return producto;
        }
    } 
}
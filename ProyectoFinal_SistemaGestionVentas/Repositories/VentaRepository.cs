using System.Data;
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

        public List<Venta> ListarVentas()
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
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Venta venta = ObtenerVentaDesdeReader(reader);
                                lista.Add(venta);
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

        public Venta? ObtenerVenta(int Id)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida.");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Venta WHERE Id = @Id", connection))
                {
                    connection.Open();
                    cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt) { Value = Id });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Venta venta = ObtenerVentaDesdeReader(reader);
                            return venta;
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

        public Venta? CrearVenta(Venta venta)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida.");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Venta(Comentarios, IdUsuario) VALUES(@Comentarios, @IdUsuario); SELECT @@Identity ", connection))
                {
                    connection.Open();
                    cmd.Parameters.Add(new SqlParameter("Comentarios", SqlDbType.VarChar) { Value = venta.Comentarios });
                    cmd.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = venta.IdUsuario });
                    venta.Id = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    return venta;
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

        public Venta? ModificarVenta(int Id, Venta ventaParaActualizar)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida.");
            }
            try
            {
                Venta? venta = ObtenerVenta(Id);
                if (venta == null)
                {
                    return null;
                }

                List<string> camposParaActualizar = new List<string>();
                if (venta.Comentarios != ventaParaActualizar.Comentarios && !string.IsNullOrEmpty(ventaParaActualizar.Comentarios))
                {
                    camposParaActualizar.Add("Comentarios = @Comentarios");
                    venta.Comentarios = ventaParaActualizar.Comentarios;
                }
                if (camposParaActualizar.Count == 0)
                {
                    throw new Exception("No New Fields To Update.");
                }

                using (SqlCommand cmd = new SqlCommand($"UPDATE Venta SET {String.Join(", ", camposParaActualizar)} WHERE Id = @Id", connection))
                {
                    cmd.Parameters.Add(new SqlParameter("Comentarios", SqlDbType.VarChar) { Value = venta.Comentarios });
                    cmd.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = venta.IdUsuario });
                    cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt) { Value = Id });
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return venta;
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

        public bool EliminarVenta(int Id)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida.");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Venta WHERE Id = @Id", connection))
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

        public static Venta ObtenerVentaDesdeReader(SqlDataReader reader)
        {
            Venta venta = new Venta();
            venta.Id = Convert.ToInt32(reader["Id"].ToString());
            venta.Comentarios = reader["Comentarios"].ToString();
            venta.IdUsuario = Convert.ToInt32(reader["IdUsuario"].ToString());
            return venta;
        }
    }
}
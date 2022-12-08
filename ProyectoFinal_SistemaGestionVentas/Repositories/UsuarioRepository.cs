using System.Data;
using System.Data.SqlClient;
using ProyectoFinal_SistemaGestionVentas.Models;

namespace ProyectoFinal_SistemaGestionVentas.Repositories
{
    public class UsuarioRepository
    {
        private SqlConnection? connection;
        private string? connectionString = "Server=sql.bsite.net\\MSSQL2016;" +
            "Database=camcamwtf_sistema_gestion_csharp;" +
            "User Id=camcamwtf_sistema_gestion_csharp;" +
            "Password=072032";

        public UsuarioRepository()
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

        public List<Usuario> ListarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();

            if (connection == null)
            {
                throw new Exception("Conexión no establecida.");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario", connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Usuario usuario = ObtenerUsuarioDesdeReader(reader);
                                lista.Add(usuario);
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

        public Usuario? ObtenerUsuario(int Id)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida.");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario WHERE Id = @Id", connection))
                {
                    connection.Open();
                    cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt) { Value = Id });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows) 
                        {
                            reader.Read();
                            Usuario usuario = ObtenerUsuarioDesdeReader(reader);
                            return usuario;
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

        public Usuario CrearUsuario(Usuario usuario) 
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida.");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Usuario(Nombre, Apellido, NombreUsuario, Contrasenia, Mail) VALUES(@Nombre, @Apellido, @NombreUsuario, @Contrasenia, @Mail); SELECT @@Identity ", connection))
                {
                    connection.Open();
                    cmd.Parameters.Add(new SqlParameter("Nombre", SqlDbType.VarChar) { Value = usuario.Nombre });
                    cmd.Parameters.Add(new SqlParameter("Apellido", SqlDbType.VarChar) { Value = usuario.Apellido });
                    cmd.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario });
                    cmd.Parameters.Add(new SqlParameter("Contrasenia", SqlDbType.VarChar) { Value = usuario.Contrasenia });
                    cmd.Parameters.Add(new SqlParameter("Mail", SqlDbType.VarChar) { Value = usuario.Mail });
                    usuario.Id = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    return usuario;
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

        public Usuario? ModificarUsuario(int Id, Usuario usuarioParaActualizar)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida.");
            }
            try
            {
                Usuario? usuario = ObtenerUsuario(Id);
                if (usuario == null)
                {
                    return null;
                }

                List<string> camposParaActualizar = new List<string>();
                if (usuario.Nombre != usuarioParaActualizar.Nombre && !string.IsNullOrEmpty(usuarioParaActualizar.Nombre))
                {
                    camposParaActualizar.Add("Nombre = @Nombre");
                    usuario.Nombre = usuarioParaActualizar.Nombre;
                }
                if (usuario.Apellido != usuarioParaActualizar.Apellido && !string.IsNullOrEmpty(usuarioParaActualizar.Apellido))
                {
                    camposParaActualizar.Add("Apellido = @Apellido");
                    usuario.Apellido = usuarioParaActualizar.Apellido;
                }
                if (usuario.NombreUsuario != usuarioParaActualizar.NombreUsuario && !string.IsNullOrEmpty(usuarioParaActualizar.NombreUsuario))
                {
                    camposParaActualizar.Add("NombreUsuario = @NombreUsuario");
                    usuario.NombreUsuario = usuarioParaActualizar.NombreUsuario;
                }
                if (usuario.Contrasenia != usuarioParaActualizar.Contrasenia && !string.IsNullOrEmpty(usuarioParaActualizar.Contrasenia))
                {
                    camposParaActualizar.Add("Contrasenia = @Contrasenia");
                    usuario.Contrasenia = usuarioParaActualizar.Contrasenia;
                }
                if (usuario.Mail != usuarioParaActualizar.Mail && !string.IsNullOrEmpty(usuarioParaActualizar.Mail))
                {
                    camposParaActualizar.Add("Mail = @Mail");
                    usuario.Mail = usuarioParaActualizar.Mail;
                }
                if (camposParaActualizar.Count == 0)
                {
                    throw new Exception("No New Fields To Update.");
                }

                using (SqlCommand cmd = new SqlCommand($"UPDATE Usuario SET {String.Join(", ", camposParaActualizar)} WHERE Id = @Id", connection))
                {
                    cmd.Parameters.Add(new SqlParameter("Nombre", SqlDbType.VarChar) { Value = usuario.Nombre });
                    cmd.Parameters.Add(new SqlParameter("Apellido", SqlDbType.VarChar) { Value = usuario.Apellido });
                    cmd.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario });
                    cmd.Parameters.Add(new SqlParameter("Contrasenia", SqlDbType.VarChar) { Value = usuario.Contrasenia });
                    cmd.Parameters.Add(new SqlParameter("Mail", SqlDbType.VarChar) { Value = usuario.Mail });
                    cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt) { Value = Id });
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return usuario;
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

        public bool EliminarUsuario(int Id)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida.");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Usuario WHERE Id = @Id", connection))
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

        private static Usuario ObtenerUsuarioDesdeReader(SqlDataReader reader)
        {
            Usuario usuario = new Usuario();
            usuario.Id = Convert.ToInt32(reader["Id"].ToString());
            usuario.Nombre = reader["Nombre"].ToString();
            usuario.Apellido = reader["Apellido"].ToString();
            usuario.NombreUsuario = reader["NombreUsuario"].ToString();
            usuario.Contrasenia = reader["Contrasenia"].ToString();
            usuario.Mail = reader["Mail"].ToString();
            return usuario;
        }
    }
}
using ProyectoFinalWebAPP.Model;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinalWebAPP.Repositoy
{
    public class UsuarioHandler : DbHandler
    {
        private Usuario LeerUsuario(SqlDataReader dataReader)
        {
            Usuario usuario = new Usuario(Convert.ToInt32(dataReader["Id"]), dataReader["Nombre"].ToString(), dataReader["Apellido"].ToString(), dataReader["NombreUsuario"].ToString(), dataReader["Contraseña"].ToString(), dataReader["Mail"].ToString());

            return usuario;
        }

        public Usuario Get(long id)
        {
            Usuario usuario = new Usuario();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "SELECT * FROM [SistemaGestion].[dbo].[Usuario] WHERE Id = @id";
                    sqlCommand.Parameters.AddWithValue("@id", id);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows & dataReader.Read()) //verifico que haya filas y que data reader haya leido
                        {
                            usuario = LeerUsuario(dataReader);
                        }
                    }

                    sqlConnection.Close();
                }
            }

            return usuario;
        }

        public List<Usuario> Get()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [SistemaGestion].[dbo].[Usuario]", sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows) //verifico que haya filas
                        {
                            while (dataReader.Read())
                            {
                                usuarios.Add(LeerUsuario(dataReader));
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }

            return usuarios;
        }

        public Usuario GetByCotraseña(string userName, string userContrseña)
        {
            Usuario usuario = new Usuario();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "SELECT * FROM [SistemaGestion].[dbo].[Usuario] WHERE NombreUsuario = @nombreUsuario AND Contraseña = @contraseña";
                    sqlCommand.Parameters.AddWithValue("@nombreUsuario", userName);
                    sqlCommand.Parameters.AddWithValue("@contraseña", userContrseña);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows & dataReader.Read()) //verifico que haya filas y que data reader haya leido
                        {
                            usuario = LeerUsuario(dataReader);
                        }
                    }

                    sqlConnection.Close();
                }
            }

            return usuario;
        }


        public void Delete(long id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM [SistemaGestion].[dbo].[Usuario] WHERE Id = @id";

                SqlParameter sqlParameter = new SqlParameter("id", SqlDbType.BigInt);
                sqlParameter.Value = id;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(sqlParameter);
                    sqlCommand.ExecuteScalar(); // ejecuta el delete
                }

                sqlConnection.Close();
            }
        }

        public void Add(Usuario usuario)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "INSERT INTO [SistemaGestion].[dbo].[Usuario] (Nombre, Apellido, NombreUsuario, Contraseña, Mail) VALUES (@Nombre, @Apellido, @NombreUsuario, @Contraseña, @Mail);";

                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("Nombre", SqlDbType.VarChar) { Value = usuario.Nombre },
                    new SqlParameter("Apellido", SqlDbType.VarChar) { Value = usuario.Apellido },
                    new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario },
                    new SqlParameter("Contraseña", SqlDbType.VarChar) { Value = usuario.Contraseña },
                    new SqlParameter("Mail", SqlDbType.VarChar) { Value = usuario.Mail }
                };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    foreach (SqlParameter paramter in parameters)
                    {
                        sqlCommand.Parameters.Add(paramter);
                    }

                    sqlCommand.ExecuteNonQuery(); // ejecuta el insert
                }

                sqlConnection.Close();
            }
        }
    }
}

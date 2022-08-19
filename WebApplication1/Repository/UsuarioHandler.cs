using ProyectoFinalWebAPP.Model;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinalWebAPP.Repository
{
    public static class UsuarioHandler
    {
        public const string ConnectionString = "Server=DESKTOP-MMRH9QD;Database=SistemaGestion;Trusted_Connection=True";

        private static Usuario LeerUsuario(SqlDataReader dataReader)
        {
            Usuario usuario = new Usuario(Convert.ToInt32(dataReader["Id"]), dataReader["Nombre"].ToString(), dataReader["Apellido"].ToString(), dataReader["NombreUsuario"].ToString(), dataReader["Contraseña"].ToString(), dataReader["Mail"].ToString());

            return usuario;
        }

        public static Usuario Get(string nombreUsuario)
        {
            Usuario usuario = new Usuario();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "SELECT * FROM [SistemaGestion].[dbo].[Usuario] WHERE NombreUsuario = @nombreUsuario";
                    sqlCommand.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);

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

        //public static List<Usuario> Get()
        //{
        //    List<Usuario> usuarios = new List<Usuario>();

        //    using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
        //    {
        //        using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [SistemaGestion].[dbo].[Usuario]", sqlConnection))
        //        {
        //            sqlConnection.Open();

        //            using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
        //            {
        //                if (dataReader.HasRows) //verifico que haya filas
        //                {
        //                    while (dataReader.Read())
        //                    {
        //                        usuarios.Add(LeerUsuario(dataReader));
        //                    }
        //                }
        //            }

        //            sqlConnection.Close();
        //        }
        //    }

        //    return usuarios;
        //}

        public static bool login(string userName, string userContrseña)
        {           
            bool resultado = false;

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
                            resultado = true;
                        }                       
                    }

                    sqlConnection.Close();
                }
            }

            return resultado;
        }

        public static bool Delete(long id)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM [SistemaGestion].[dbo].[Usuario] WHERE Id = @id";

                SqlParameter sqlParameter = new SqlParameter("id", SqlDbType.BigInt);
                sqlParameter.Value = id;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(sqlParameter);
                    
                    int numerosDeRegistros = sqlCommand.ExecuteNonQuery(); // ejecuta el delete

                    if (numerosDeRegistros > 0)
                    {
                        resultado = true;
                    }
                }

                sqlConnection.Close();
            }
            return resultado;
        }

        public static bool Add(Usuario usuario)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "INSERT INTO [SistemaGestion].[dbo].[Usuario] (Nombre, Apellido, NombreUsuario, Contraseña, Mail) VALUES (@Nombre, @apellido, @NombreUsuario, @contraseña, @mail);";

                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuario.Nombre },
                    new SqlParameter("apellido", SqlDbType.VarChar) { Value = usuario.Apellido },
                    new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario },
                    new SqlParameter("contraseña", SqlDbType.VarChar) { Value = usuario.Contraseña },
                    new SqlParameter("mail", SqlDbType.VarChar) { Value = usuario.Mail }
                };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    foreach (SqlParameter paramter in parameters)
                    {
                        sqlCommand.Parameters.Add(paramter);
                    }

                    int numerosDeRegistros = sqlCommand.ExecuteNonQuery(); // ejecuta el insert

                    if (numerosDeRegistros > 0)
                    {
                        resultado = true;
                    } 
                }

                sqlConnection.Close();
            }
            return resultado;
        }

        public static bool Update(Usuario usuario)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "UPDATE [SistemaGestion].[dbo].[Usuario] SET Nombre = '@nombre', Apellido = '@apellido', NombreUsuario = '@nombreUsuario', Contraseña = '@contraseña', Mail = '@mail' WHERE Id = @id";

                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuario.Nombre },
                    new SqlParameter("apellido", SqlDbType.VarChar) { Value = usuario.Apellido },
                    new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario },
                    new SqlParameter("contraseña", SqlDbType.VarChar) { Value = usuario.Contraseña },
                    new SqlParameter("mail", SqlDbType.VarChar) { Value = usuario.Mail },
                    new SqlParameter("id", SqlDbType.VarChar) { Value = usuario.Id }
                };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    foreach (SqlParameter paramter in parameters)
                    {
                        sqlCommand.Parameters.Add(paramter);
                    }

                    int numerosDeRegistros = sqlCommand.ExecuteNonQuery(); // Se ejecuta update

                    if (numerosDeRegistros > 0)
                    {
                        resultado = true;
                    }
                }

                sqlConnection.Close();
            }

            return resultado;
        }
    }
    
}

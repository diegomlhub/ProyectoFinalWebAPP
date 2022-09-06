using ProyectoFinalWebAPP.Model;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinalWebAPP.Repository
{
    public static class VentaHandler
    {
        public const string ConnectionString = "Server=DESKTOP-MMRH9QD;Database=SistemaGestion;Trusted_Connection=True";
        //Metodo interno LeerVenta() para ahorrar lineas de datareader y parameters.
        private static Venta LeerVenta(SqlDataReader dataReader)
        {
            Venta venta = new Venta(Convert.ToInt32(dataReader["Id"]), dataReader["Comentarios"].ToString());

            return venta;
        }

        public static List<Venta> Get(long idUsuario)
        {
            List<Venta> ventas = new List<Venta>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "SELECT v.Id, v.Comentarios FROM [SistemaGestion].[dbo].[ProductoVendido] AS pv INNER JOIN [SistemaGestion].[dbo].[Producto] AS p ON p.Id = pv.IdProducto INNER JOIN [SistemaGestion].[dbo].[Venta] AS v ON v.Id = pv.IdVenta WHERE IdUsuario = @idUsuario;";
                    sqlCommand.Parameters.AddWithValue("@idUsuario", idUsuario);
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows) //verifico que haya filas
                        {
                            while (dataReader.Read())
                            {
                                ventas.Add(LeerVenta(dataReader));
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }

            return ventas;
        }

        public static bool Delete(long id)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM [SistemaGestion].[dbo].[Venta] WHERE Id = @id;";

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

        public static bool Add(Venta venta)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "INSERT INTO [SistemaGestion].[dbo].[Venta] (Comentarios) VALUES (@Comentarios);";

                SqlParameter parameters = new SqlParameter("Comentarios", SqlDbType.VarChar) { Value = venta.Comentarios };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(parameters);
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

        public static bool Update(Venta venta)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "UPDATE [SistemaGestion].[dbo].[Venta] SET Comentarios = @comentarios WHERE Id = @id";

                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("id", SqlDbType.BigInt) { Value = venta.Id },
                    new SqlParameter("comentarios", SqlDbType.VarChar) { Value = venta.Comentarios }
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

        public static long IdUltimaVenta()
        {   
            Venta venta = new Venta();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "SELECT TOP(1) * FROM Venta ORDER BY Id DESC;"; //selecciono id de ultima venta cargada            
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows & dataReader.Read()) //verifico que haya filas
                        {
                            venta.Id = Convert.ToInt32(dataReader["Id"]);                            
                        }
                    }

                    sqlConnection.Close();
                }
            }

            return venta.Id;
        }
    }
}

using ProyectoFinalWebAPP.Model;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinalWebAPP.Repository
{
    public static class ProductoHandler
    {
        public const string ConnectionString = "Server=DESKTOP-MMRH9QD;Database=SistemaGestion;Trusted_Connection=True";
        //Metodo interno LeerProducto() para ahorrar lineas de datareader y parameters.
        public static Producto LeerProducto(SqlDataReader dataReader)
        {
            Producto producto = new Producto(Convert.ToInt32(dataReader["Id"]), dataReader["Descripciones"].ToString(), Convert.ToInt32(dataReader["Costo"]), Convert.ToInt32(dataReader["PrecioVenta"]), Convert.ToInt32(dataReader["Stock"]), Convert.ToInt32(dataReader["IdUsuario"]));

            return producto;
        }

        public static List<Producto> Get(long idUsuario)
        {
            List<Producto> productos = new List<Producto>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "SELECT * FROM [SistemaGestion].[dbo].[Producto] WHERE IdUsuario = @idUsuario";
                    sqlCommand.Parameters.AddWithValue("@idUsuario", idUsuario);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows) //verifico que haya filas
                        {
                            while (dataReader.Read())
                            {
                                productos.Add(LeerProducto(dataReader));
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }

            return productos;
        }
                
        public static bool Delete(long id)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM [SistemaGestion].[dbo].[Producto] WHERE Id = @id";

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

        public static bool Add(Producto producto)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "INSERT INTO [SistemaGestion].[dbo].[Producto] (Descripciones, Costo, PrecioVenta, Stock, IdUsuario) VALUES (@Descripciones, @Costo, @PrecioVenta, @Stock, @IdUsuario);";

                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("Descripciones", SqlDbType.VarChar) { Value = producto.Descripciones },
                    new SqlParameter("Costo", SqlDbType.Money) { Value = producto.Costo },
                    new SqlParameter("PrecioVenta", SqlDbType.Money) { Value = producto.PrecioVenta },
                    new SqlParameter("Stock", SqlDbType.Int) { Value = producto.Stock },
                    new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = producto.IdUsuario }
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

        public static bool Update(Producto producto)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "UPDATE [SistemaGestion].[dbo].[Producto] SET Descripciones = @descripciones, Costo = @costo, PrecioVenta = @precioVenta, Stock = @stock, IdUsuario = @idUsuario WHERE Id = @id";

                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("id", SqlDbType.BigInt) { Value = producto.Id },
                    new SqlParameter("descripciones", SqlDbType.VarChar) { Value = producto.Descripciones },
                    new SqlParameter("costo", SqlDbType.Money) { Value = producto.Costo },
                    new SqlParameter("precioVenta", SqlDbType.Money) { Value = producto.PrecioVenta },
                    new SqlParameter("stock", SqlDbType.Int) { Value = producto.Stock },
                    new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = producto.IdUsuario }                    
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

using ProyectoFinalWebAPP.Model;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinalWebAPP.Repository
{
    public static class ProductoVendidoHandler
    {
        public const string ConnectionString = "Server=DESKTOP-MMRH9QD;Database=SistemaGestion;Trusted_Connection=True";

        private static ProductoVendido LeerProductoVendido(SqlDataReader dataReader)
        {
            ProductoVendido productoVendido = new ProductoVendido(Convert.ToInt32(dataReader["Id"]), Convert.ToInt32(dataReader["Stock"]), Convert.ToInt32(dataReader["IdProducto"]), Convert.ToInt32(dataReader["IdVenta"]));

            return productoVendido;
        }
               
        public static List<ProductoVendido> Get(long idUsuario)
        {
            List<ProductoVendido> productosVendidos = new List<ProductoVendido>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "SELECT pv.Id, pv.Stock, pv.IdProducto, pv.IdVenta FROM[SistemaGestion].[dbo].[ProductoVendido] AS pv INNER JOIN[SistemaGestion].[dbo].[Producto] AS p ON pv.IdProducto = p.Id WHERE IdUsuario = @idUsuario;";
                    sqlCommand.Parameters.AddWithValue("@idUsuario", idUsuario);
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows) //verifico que haya filas
                        {
                            while (dataReader.Read())
                            {
                                productosVendidos.Add(LeerProductoVendido(dataReader));
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }

            return productosVendidos;
        }

        public static bool Delete(long id)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM [SistemaGestion].[dbo].[ProductoVendido] WHERE Id = @id";

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

        public static bool Add(ProductoVendido productoVendido)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "INSERT INTO [SistemaGestion].[dbo].[ProductoVendido] (Stock, IdProducto, IdVenta) VALUES (@Stock, @IdProducto, @IdVenta);";

                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("Stock", SqlDbType.Int) { Value = productoVendido.Stock },
                    new SqlParameter("IdProducto", SqlDbType.Int) { Value = productoVendido.IdProducto },
                    new SqlParameter("IdVenta", SqlDbType.Int) { Value = productoVendido.IdVenta },
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

        public static bool Update(ProductoVendido productoVendido)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "UPDATE [SistemaGestion].[dbo].[ProductoVendido] SET Stock = @stock, IdProducto = @idProducto, idVenta = @idVenta WHERE Id = @id;";

                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("id", SqlDbType.BigInt) { Value = productoVendido.Id },
                    new SqlParameter("stock", SqlDbType.BigInt) { Value = productoVendido.Stock },
                    new SqlParameter("idProducto", SqlDbType.BigInt) { Value = productoVendido.IdProducto },
                    new SqlParameter("idVenta", SqlDbType.BigInt) { Value = productoVendido.IdVenta }
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
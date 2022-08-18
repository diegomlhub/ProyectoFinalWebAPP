using ProyectoFinalWebAPP.Model;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinalWebAPP.Repositoy
{
    public static class ProductoVendidoHandler
    {
        public const string ConnectionString = "Server=DESKTOP-MMRH9QD;Database=SistemaGestion;Trusted_Connection=True";

        private static ProductoVendido LeerProductoVendido(SqlDataReader dataReader)
        {
            ProductoVendido productoVendido = new ProductoVendido(Convert.ToInt32(dataReader["Id"]), Convert.ToInt32(dataReader["Stock"]), Convert.ToInt32(dataReader["IdProducto"]), Convert.ToInt32(dataReader["IdVenta"]));

            return productoVendido;
        }

        public static ProductoVendido Get(long id)
        {
            ProductoVendido productoVendido = new ProductoVendido();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "SELECT * FROM [SistemaGestion].[dbo].[ProductoVendido] WHERE Id = @id";
                    sqlCommand.Parameters.AddWithValue("@id", id);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows & dataReader.Read()) //verifico que haya filas y que data reader haya leido
                        {
                            productoVendido = LeerProductoVendido(dataReader);
                        }
                    }

                    sqlConnection.Close();
                }
            }

            return productoVendido;
        }

        public static List<ProductoVendido> Get()
        {
            List<ProductoVendido> productosVendidos = new List<ProductoVendido>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [SistemaGestion].[dbo].[ProductoVendido]", sqlConnection))
                {
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

        public static void Delete(long id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM [SistemaGestion].[dbo].[ProductoVendido] WHERE Id = @id";

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

        public static void Add(ProductoVendido productoVendido)
        {
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
                    foreach (SqlParameter parameter in parameters)
                    {
                        sqlCommand.Parameters.Add(parameter);
                    }

                    sqlCommand.ExecuteNonQuery(); // ejecuta el insert
                }

                sqlConnection.Close();
            }

        }
    }
}
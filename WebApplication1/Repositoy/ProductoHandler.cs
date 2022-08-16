using ProyectoFinalWebAPP.Model;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinalWebAPP.Repositoy
{
    public class ProductoHandler : DbHandler
    {
        private Producto LeerProducto(SqlDataReader dataReader)
        {
            Producto producto = new Producto(Convert.ToInt32(dataReader["Id"]), dataReader["Descripciones"].ToString(), Convert.ToInt32(dataReader["Costo"]), Convert.ToInt32(dataReader["PrecioVenta"]), Convert.ToInt32(dataReader["Stock"]), Convert.ToInt32(dataReader["IdUsuario"]));

            return producto;
        }

        public Producto Get(long id)
        {
            Producto producto = new Producto();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "SELECT * FROM [SistemaGestion].[dbo].[Producto] WHERE Id = @id";
                    sqlCommand.Parameters.AddWithValue("@id", id);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows & dataReader.Read()) //verifico que haya filas y que data reader haya leido
                        {
                            producto = LeerProducto(dataReader);
                        }
                    }

                    sqlConnection.Close();
                }
            }

            return producto;
        }

        public List<Producto> Get()
        {
            List<Producto> productos = new List<Producto>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [SistemaGestion].[dbo].[Producto]", sqlConnection))
                {
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

        public void Delete(long id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM [SistemaGestion].[dbo].[Producto] WHERE Id = @id";

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

        public void Add(Producto producto)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "INSERT INTO [SistemaGestion].[dbo].[Producto] (Descripciones, Costo, PrecioVenta, Stock, IdUsuario) VALUES (@Descripciones, @Costo, @PrecioVenta, @Stock, @IdUsuario);";

                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("Descripciones", SqlDbType.VarChar) { Value = producto.Descripciones },
                    new SqlParameter("Costo", SqlDbType.Int) { Value = producto.Costo },
                    new SqlParameter("PrecioVenta", SqlDbType.Int) { Value = producto.PrecioVenta },
                    new SqlParameter("Stock", SqlDbType.Int) { Value = producto.Stock },
                    new SqlParameter("IdUsuario", SqlDbType.Int) { Value = producto.IdUsuario }
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

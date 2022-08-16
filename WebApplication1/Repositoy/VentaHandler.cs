using ProyectoFinalWebAPP.Model;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinalWebAPP.Repositoy
{
    public class VentaHandler : DbHandler
    {
        private Venta LeerVenta(SqlDataReader dataReader)
        {
            Venta venta = new Venta(Convert.ToInt32(dataReader["Id"]), dataReader["Comentarios"].ToString());

            return venta;
        }

        public Venta Get(long id)
        {
            Venta venta = new Venta();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "SELECT * FROM [SistemaGestion].[dbo].[Venta] WHERE Id = @id";
                    sqlCommand.Parameters.AddWithValue("@id", id);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows & dataReader.Read()) //verifico que haya filas y que data reader haya leido
                        {
                            venta = LeerVenta(dataReader);
                        }
                    }

                    sqlConnection.Close();
                }
            }

            return venta;
        }

        public List<Venta> Get()
        {
            List<Venta> ventas = new List<Venta>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [SistemaGestion].[dbo].[Venta]", sqlConnection))
                {
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

        public void Delete(long id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM [SistemaGestion].[dbo].[Venta] WHERE Id = @id";

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

        public void Add(Venta venta)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "INSERT INTO [SistemaGestion].[dbo].[Venta] (Comentarios) VALUES (@Comentarios);";

                SqlParameter parameters = new SqlParameter("Comentarios", SqlDbType.VarChar) { Value = venta.Comentarios };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(parameters);
                    sqlCommand.ExecuteNonQuery(); // ejecuta el insert
                }

                sqlConnection.Close();
            }
        }
    }
}

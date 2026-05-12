using Microsoft.Data.SqlClient;

namespace LogisticsWarehouse.Data
{
    public class DatabaseConnection
    {
        private string connectionString = "Server=DESKTOP-N6PC6UF\\SQLEXPRESS;Database=smart_warehouse;Integrated Security=true;";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
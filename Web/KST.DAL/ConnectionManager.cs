using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;


namespace KST.DAL
{
    /// <summary>
    /// Manager class for database connection.
    /// </summary>
    public class ConnectionManager
    {
        private static string connectionString;

        /// <summary>
        /// The database connection string.
        /// </summary>
        public static string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        /// <summary>
        /// Gets a opened database connection.
        /// </summary>
        public static DbConnection OpenConnection
        {
            get
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                return connection;
            }
        }
    }
}

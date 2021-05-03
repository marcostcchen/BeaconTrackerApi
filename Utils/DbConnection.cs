using System.Data;
using System.Data.SqlClient;

namespace BeaconTrackerApi.Utils
{
    public class DbConnection
    {
        private SqlConnection _connection;

        public SqlConnection OpenConnection()
        {
            var builder = new SqlConnectionStringBuilder {DataSource = @"localhost\SQLEXPRESS01", UserID = "user", Password = "user", InitialCatalog = "beacontracker"};
            // var builder = new SqlConnectionStringBuilder {DataSource = "beacontrackerdb.clmsc6yg9pqz.us-east-2.rds.amazonaws.com,1433", UserID = "admin", Password = "Natalia123", InitialCatalog = "beacontrackerdb"};
            _connection = new SqlConnection(builder.ConnectionString);
            if (_connection.State != ConnectionState.Open) _connection.Open();
            return _connection;
        }

        public void CloseConnection()
        {
            if (_connection == null) return;
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}
using System.Data;
using System.Data.SqlClient;

namespace BeaconTrackerApi.Database
{
    public class DbConnection
    {
        private SqlConnection _connection;

        public SqlConnection OpenConnection()
        {
            var builder = new SqlConnectionStringBuilder {DataSource = @"localhost\SQLEXPRESS01", UserID = "user", Password = "user", InitialCatalog = "beacontracker"};
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
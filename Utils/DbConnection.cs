using System.Data;
using System.Data.SqlClient;

namespace BeaconTrackerApi.Utils
{
    public class DbConnection
    {
        private SqlConnection _connection;

        public SqlConnection OpenConnection()
        {
            var builder = new SqlConnectionStringBuilder {DataSource = @"ec2-3-14-80-158.us-east-2.compute.amazonaws.com", UserID = "sa", Password = "Teste@132", InitialCatalog = "BeaconTracker"};
            // var builder = new SqlConnectionStringBuilder {DataSource = @"localhost\SQLEXPRESS01", UserID = "user", Password = "user", InitialCatalog = "beacontracker"};
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
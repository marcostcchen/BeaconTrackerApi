using System;
using System.Data;
using System.Data.SqlClient;
using BeaconTrackerApi.Model;
using BeaconTrackerApi.Utils;

namespace BeaconTrackerApi.Database
{
    public class BeaconDAO
    {
        public void InsertMeasure(SendRSSIBeaconIn beaconIn)
        {
            var db = new DbConnection();

            var cmd = new SqlCommand("[PR_Update_User_Location]", db.OpenConnection())
                {CommandTimeout = 99999, CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@idUser", beaconIn.idUser);
            cmd.Parameters.AddWithValue("@RSSIBeaconId1", beaconIn.RSSIBeaconId1);
            cmd.Parameters.AddWithValue("@RSSIBeaconId2", beaconIn.RSSIBeaconId2);
            cmd.Parameters.AddWithValue("@RSSIBeaconId3", beaconIn.RSSIBeaconId3);
            cmd.Parameters.AddWithValue("@measureTime", beaconIn.measureTime);
            cmd.ExecuteReader();
            
            db.CloseConnection();
            cmd.Dispose();
        }
    }
}
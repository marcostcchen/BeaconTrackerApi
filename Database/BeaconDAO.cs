using System;
using System.Data;
using System.Data.SqlClient;
using BeaconTrackerApi.Model;
using BeaconTrackerApi.Model.In;
using BeaconTrackerApi.Utils;

namespace BeaconTrackerApi.Database
{
    public class BeaconDAO
    {
        public void InsertMeasure(SendRSSIBeaconIn beaconIn)
        {
            var db = new DbConnection();
            
            foreach (var beacon in beaconIn.beaconList)
            {
                var cmd = new SqlCommand("[PR_Insert_Measure]", db.OpenConnection())
                    {CommandTimeout = 99999, CommandType = CommandType.StoredProcedure};
                cmd.Parameters.AddWithValue("@idBeacon", beacon.idBeacon);
                cmd.Parameters.AddWithValue("@idUser", beaconIn.idUser);
                cmd.Parameters.AddWithValue("@RSSI", beacon.rssi);
                cmd.ExecuteReader();
                db.CloseConnection();    
            }
        }
    }
}
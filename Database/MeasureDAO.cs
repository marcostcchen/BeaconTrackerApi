using System;
using System.Data;
using System.Data.SqlClient;
using BeaconTrackerApi.Model.In;

namespace BeaconTrackerApi.Database
{
    public class MeasureDAO
    {
        public void InsertMeasure(MeasureIn measureIn)
        {
            var db = new DbConnection();
            var datetime = DateTime.Now;
            
            foreach (var measure in measureIn.measures)
            {
                var cmd = new SqlCommand("[PR_Insert_Measure]", db.OpenConnection())
                    {CommandTimeout = 99999, CommandType = CommandType.StoredProcedure};
                cmd.Parameters.AddWithValue("@idBeacon", measure.idBeacon);
                cmd.Parameters.AddWithValue("@idUser", measure.idUser);
                cmd.Parameters.AddWithValue("@RSSI", measure.RSSI);
                cmd.Parameters.AddWithValue("@measureTime", datetime);
                cmd.ExecuteReader();
                db.CloseConnection();    
            }
        }
    }
}
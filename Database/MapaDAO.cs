using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using BeaconTrackerApi.Enum;
using BeaconTrackerApi.Model;
using BeaconTrackerApi.Utils;

namespace BeaconTrackerApi.Database
{
    public class MapaDAO
    {
        public Region GetRegionDetail(int idRegion)
        {
            var db = new DbConnection();
            var cmd = new SqlCommand("[PR_Detalhes_Region]", db.OpenConnection())
                {CommandTimeout = 99999, CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@@idRegion", idRegion);

            var reader = cmd.ExecuteReader();
            if (!reader.HasRows) throw new SqlNullValueException("Região não encontrado!");
            reader.Read();

            var region = new Region()
            {
                idRegion = Convert.ToInt32(reader["idRegion"]),
                nome = reader["nome"].ToString(),
                description = reader["description"].ToString(),
                avgTemperature = Convert.ToInt32(reader["avgTemperature"]),
                dangerLevel = (DangerLevel) Convert.ToInt32(reader["dangerLevel"]),
                maxStayTimeMinutes = Convert.ToInt32(reader["maxStayTimeMinutes"])
            };

            db.CloseConnection();
            return region;
        }
    }
}
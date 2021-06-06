using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using BeaconTrackerApi.Enum;
using BeaconTrackerApi.Model;
using BeaconTrackerApi.Utils;

namespace BeaconTrackerApi.Database
{
    public class LoginDAO
    {
        public User GetUsers(string login, string password)
        {
            var db = new DbConnection();
            var cmd = new SqlCommand("[PR_Efetuar_Login]", db.OpenConnection())
                {CommandTimeout = 99999, CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@password", password);
            
            var reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                cmd.Dispose();
                reader.Close();
                db.CloseConnection();
                throw new SqlNullValueException("Login não encontrado!");
            }
            
            reader.Read();

            var user = new User
            {
                idUser = Convert.ToInt32(reader["idUser"]),
                login = reader["login"].ToString(),
                name = reader["name"].ToString(),
                idRole = (Roles) Convert.ToInt32(reader["idRole"]),
            };
            
            cmd.Dispose();
            reader.Close();
            db.CloseConnection();
            return user;
        }
    }
}
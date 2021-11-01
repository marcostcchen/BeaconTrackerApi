using System;
using System.Collections.Generic;
using System.Linq;
using BeaconTrackerApi.Database.Settings;
using BeaconTrackerApi.Model;
using MongoDB.Driver;

namespace BeaconTrackerApi.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _user;

        public UserService(IMongoCollectionSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _user = database.GetCollection<User>("User");
        }

        public List<User> GetUsers()
        {
            var users = _user.Find(user => true).ToList();
            return users;
        }

        public void UpdateUserRSSI(string userId, BeaconRSSI beaconRssi)
        {
            var user = _user.Find(u => u.Id == userId).FirstOrDefault();
            user.beaconsRSSI.Add(beaconRssi);
            user.lastLocation = beaconRssi;
            _user.ReplaceOne(u => u.Id == userId, user);
        }

        public void UpdateUserIdOneSignal(string userId, string userId_OneSignal)
        {
            var user = _user.Find(u => u.Id == userId).FirstOrDefault();
            user.userId_OneSignal = userId_OneSignal;
            _user.ReplaceOne(u => u.Id == userId, user);
        }

        public void CreateWorkSession(string userId, WorkSession workSession)
        {
            var user = _user.Find(u => u.Id == userId).FirstOrDefault();
            user.workSessions.Add(workSession);
        }
        
        public List<UserWorkSession> GetWorkingSessions()
        {
            var usersWorkSessions = new List<UserWorkSession>();
            var users = _user.Find(u => true).ToList();

            users.ForEach(user =>
            {
                if (user.workSessions is null) return;
                var listWorkSessions = user.workSessions.OrderByDescending(workSession => workSession.timestamp);

                var userWorkSession = new UserWorkSession()
                {
                    name = user.name,
                    listWorkSessions = listWorkSessions.ToList()
                };
                usersWorkSessions.Add(userWorkSession);
            });

            return usersWorkSessions;
        }
    }
}

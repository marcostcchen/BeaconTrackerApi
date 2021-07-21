using System;
using System.Collections.Generic;
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
        public void UpdateUserStartWorking(string userId, int maxStayMinutes)
        {
            var user = _user.Find(u => u.Id == userId).FirstOrDefault();
            user.maxStayMinutes = maxStayMinutes;
            user.isWorking = true;
            user.startWorkingTime = DateTime.Now;
            _user.ReplaceOne(u => u.Id == userId, user);
        }
        
    }
}
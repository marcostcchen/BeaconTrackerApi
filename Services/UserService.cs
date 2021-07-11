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

        public List<User> GetUsers() => _user.Find(user => true).ToList();

        public void UpdateUserRSSI(string userId, BeaconRSSI beaconRssi)
        {
            var user = _user.Find(u => u.Id == userId).FirstOrDefault();
            user.beaconsRSSI.Add(beaconRssi);
            _user.ReplaceOne(user => user.Id == userId, user);
        }
    }
}
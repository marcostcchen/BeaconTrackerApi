using System.Collections.Generic;
using BeaconTrackerApi.Database.Settings;
using BeaconTrackerApi.Model;
using MongoDB.Driver;

namespace BeaconTrackerApi.Services
{
    public class UserBeaconRSSIService
    {
        private readonly IMongoCollection<UserBeaconRSSI> _collection;

        public UserBeaconRSSIService(IMongoCollectionSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<UserBeaconRSSI>("UserBeaconRSSI");
        }

        public void InserirUserBeaconRSSI(UserBeaconRSSI userBeaconRssi) =>
            _collection.InsertOne(userBeaconRssi);
    }
}
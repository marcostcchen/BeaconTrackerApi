using System.Collections.Generic;
using BeaconTrackerApi.Model;
using BeaconTrackerApi.Model.Settings;
using MongoDB.Driver;

namespace BeaconTrackerApi.Services
{
    public class BeaconService
    {
        private readonly IMongoCollection<Beacon> _beacon;
        public BeaconService(IMongoCollectionSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _beacon = database.GetCollection<Beacon>("Beacon");
        }
        
        public IEnumerable<Beacon> ListarBeacons() => _beacon.Find(beacon => true).ToList();
    }
}
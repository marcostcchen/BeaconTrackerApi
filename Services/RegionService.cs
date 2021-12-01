using System.Collections.Generic;
using BeaconTrackerApi.Model;
using BeaconTrackerApi.Model.Settings;
using MongoDB.Driver;

namespace BeaconTrackerApi.Services
{
    public class RegionService
    {
        private readonly IMongoCollection<Region> _region;

        public RegionService(IMongoCollectionSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _region = database.GetCollection<Region>("Region");
        }

        public List<Region> GetRegions()
        {
            return _region.Find(r => true).ToList();
        }
    }
}
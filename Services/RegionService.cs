using System;
using System.Collections.Generic;
using BeaconTrackerApi.Database.Settings;
using BeaconTrackerApi.Model;
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
        
        private bool isSameLocation(BeaconsRSSI beaconsRssi, BeaconsRSSI matchBeaconsRssi)
        {
            return
                 beaconsRssi.RSSIBeaconId1 == matchBeaconsRssi.RSSIBeaconId1 &&
                 beaconsRssi.RSSIBeaconId2 == matchBeaconsRssi.RSSIBeaconId2 &&
                 beaconsRssi.RSSIBeaconId3 == matchBeaconsRssi.RSSIBeaconId3;
        }
    }
}
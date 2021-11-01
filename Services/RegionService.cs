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

        public void UpdateLocationRSSI(string idRegion, BeaconsRSSI beaconsRssi)
        {
            beaconsRssi.measureTime = DateTime.Now;

            //verifica se tem regiao com msm rssi, caso sim remove
            var regions = _region.Find(r => true).ToList();
            var regionsRepeated = regions.FindAll(region =>
            {
                var regionFound = region.mapLocation.Find(map => isSameLocation(map, beaconsRssi));
                return regionFound != null;
            });
            regionsRepeated?.ForEach(r => r.mapLocation.RemoveAll(map => isSameLocation(map, beaconsRssi)));
            regionsRepeated.ForEach(rp => _region.ReplaceOne(r => r.Id == rp.Id, rp));

            //inserir novo rssi 
            var region = _region.Find(r => r.Id == idRegion).FirstOrDefault();
            region.mapLocation.Add(beaconsRssi);
            _region.ReplaceOne(r => r.Id == idRegion, region);
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
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

        public void UpdateLocationRSSI(string idRegion, BeaconRSSI beaconRssi)
        {
            beaconRssi.measureTime = DateTime.Now;

            //verifica se tem regiao com msm rssi, caso sim remove
            var regions = _region.Find(r => true).ToList();
            var regionsRepeated = regions.FindAll(region =>
            {
                var regionFound = region.mapLocation.Find(map => isSameLocation(map, beaconRssi));
                return regionFound != null;
            });
            regionsRepeated?.ForEach(r => r.mapLocation.RemoveAll(map => isSameLocation(map, beaconRssi)));
            regionsRepeated.ForEach(rp => _region.ReplaceOne(r => r.Id == rp.Id, rp));

            //inserir novo rssi 
            var region = _region.Find(r => r.Id == idRegion).FirstOrDefault();
            region.mapLocation.Add(beaconRssi);
            _region.ReplaceOne(r => r.Id == idRegion, region);
        }

        private bool isSameLocation(BeaconRSSI beaconRSSI, BeaconRSSI matchBeaconRssi)
        {
            return
                 beaconRSSI.RSSIBeaconId1 == matchBeaconRssi.RSSIBeaconId1 &&
                 beaconRSSI.RSSIBeaconId2 == matchBeaconRssi.RSSIBeaconId2 &&
                 beaconRSSI.RSSIBeaconId3 == matchBeaconRssi.RSSIBeaconId3;
        }
    }
}
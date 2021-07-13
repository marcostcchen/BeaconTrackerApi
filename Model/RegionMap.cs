using BeaconTrackerApi.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace BeaconTrackerApi.Model
{
    public class RegionMap
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DangerLevel dangerLevel { get; set; }
        public int maxStayTimeMinutes { get; set; }
        public int avgTemperature { get; set; }
        public float RSSIBeaconId1Avg { get; set; }
        public float RSSIBeaconId2Avg { get; set; }
        public float RSSIBeaconId3Avg { get; set; }
    }
}

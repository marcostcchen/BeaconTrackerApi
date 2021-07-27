using System.Collections.Generic;
using BeaconTrackerApi.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BeaconTrackerApi.Model
{
    public class Region
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DangerLevel dangerLevel { get; set; }
        public int maxStayTimeMinutes { get; set; }
        public int minRestMinutes { get; set; }
        public int avgTemperature { get; set; }
        public List<BeaconRSSI> mapLocation { get; set; }
        public int idBeaconMinRSSI { get; set; }
    }
}
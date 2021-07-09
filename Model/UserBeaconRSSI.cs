using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BeaconTrackerApi.Model
{
    public class UserBeaconRSSI
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int idUser { get; set; }
        public int RSSIBeaconId1 { get; set; }
        public int RSSIBeaconId2 { get; set; }
        public int RSSIBeaconId3 { get; set; }
        public DateTime measureTime { get; set; }
    }
}
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BeaconTrackerApi.Model
{
    public class Beacon
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int idBeacon { get; set; }
        public string name { get; set; }
    }
}
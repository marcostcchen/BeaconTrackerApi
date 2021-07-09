using System.Collections.Generic;
using BeaconTrackerApi.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BeaconTrackerApi.Model
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string name { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public int? active { get; set; }
        public Roles? idRole { get; set; }
        public List<BeaconRSSI> beaconsRssi { get; set; }
    }
}
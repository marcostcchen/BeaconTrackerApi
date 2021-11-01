using System;
using System.Collections.Generic;
using BeaconTrackerApi.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BeaconTrackerApi.Model
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string name { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public int? active { get; set; }
        public Roles? role { get; set; }
        public string userId_OneSignal { get; set; }
        public List<WorkSession> workSessions {get;set;}
    }
}
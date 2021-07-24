using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaconTrackerApi.Model
{
    public class Notification
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string userId_OneSignal { get; set; }
        public string nome { get; set; }
        public DateTime horaEnvio { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
    }
}

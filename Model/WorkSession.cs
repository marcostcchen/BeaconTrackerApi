using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeaconTrackerApi.Enum;

namespace BeaconTrackerApi.Model
{
    public class WorkSession
    {
        public string nome { get; set; }
        public WorkingStatus status { get; set; }
        public BeaconsRSSI beaconsRssi { get; set; }
        public DateTime timestamp { get; set; }
    }
}

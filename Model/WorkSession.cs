using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaconTrackerApi.Model
{
    public class WorkSession
    {
        public string nome { get; set; }
        public string regionName { get; set; }
        public int? maxStayMinutes { get; set; }
        public int? minRestMinutes { get; set; }
        public DateTime? startWorkingTime { get; set; }
        public DateTime? startRestingTime { get; set; }
        public DateTime? finishRestingTime { get; set; }
    }
}

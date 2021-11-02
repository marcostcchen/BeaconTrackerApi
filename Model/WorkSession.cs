using System;
using BeaconTrackerApi.Enum;

namespace BeaconTrackerApi.Model
{
    public class WorkSession
    {
        public DateTime startTime { get; set; }
        public WorkingStatus status { get; set; }
        public string regionName { get; set; }
        public BeaconsRSSI beaconsRssi { get; set; }
        public DateTime measureTime { get; set; }

    }
}

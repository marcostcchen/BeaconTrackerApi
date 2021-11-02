using System;
using BeaconTrackerApi.Enum;

namespace BeaconTrackerApi.Model
{
    public class WorkSession
    {
        public WorkingStatus status { get; set; }
        public int regionId { get; set; }
        public BeaconsRSSI beaconsRssi { get; set; }
        public DateTime measureTime { get; set; }

    }
}

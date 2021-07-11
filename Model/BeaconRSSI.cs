using System;
using System.Text.Json.Serialization;

namespace BeaconTrackerApi.Model
{
    public class BeaconRSSI
    {
        public int RSSIBeaconId1 { get; set; }
        public int RSSIBeaconId2 { get; set; }
        public int RSSIBeaconId3 { get; set; }
        public DateTime measureTime { get; set; }
    }
}
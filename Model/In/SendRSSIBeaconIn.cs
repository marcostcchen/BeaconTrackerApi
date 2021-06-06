using System;
using System.Collections.Generic;

namespace BeaconTrackerApi.Model
{
    public class SendRSSIBeaconIn
    {
        public int idUser { get; set; }
        
        public int RSSIBeaconId1 { get; set; } 
        public int RSSIBeaconId2 { get; set; } 
        public int RSSIBeaconId3 { get; set; }
        public DateTime measureTime { get; set; }
    }
}
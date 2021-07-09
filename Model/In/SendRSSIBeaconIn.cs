using System;
using System.Collections.Generic;

namespace BeaconTrackerApi.Model
{
    public class SendRSSIBeaconIn
    {
        public string idUser { get; set; }
        
        public int RSSIBeacon1 { get; set; } 
        public int RSSIBeacon2 { get; set; } 
        public int RSSIBeacon3 { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace BeaconTrackerApi.Model.In
{
    public class SendRSSIBeaconIn
    {
        public int idUser { get; set; }
        public List<Beacon> beaconList { get; set; }
    }
}
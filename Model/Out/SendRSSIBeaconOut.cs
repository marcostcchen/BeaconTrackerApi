using BeaconTrackerApi.Enum;

namespace BeaconTrackerApi.Model.Out
{
    public class SendRSSIBeaconOut
    {
        public Status status { get; set; }
        public string message { get; set; }
    }
}
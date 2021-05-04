using BeaconTrackerApi.Enum;

namespace BeaconTrackerApi.Model.Out
{
    public class LoginOut
    {
        public Status status { get; set; }
        public string message { get; set; }
        public string token { get; set; }
        public User user { get; set; }
    }
}
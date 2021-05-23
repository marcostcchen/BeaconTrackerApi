using BeaconTrackerApi.Enum;

namespace BeaconTrackerApi.Model.Out
{
    public class LoginOut: BaseOut
    {
        public string token { get; set; }
        public User user { get; set; }
    }
}
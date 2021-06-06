
namespace BeaconTrackerApi.Model
{
    public class LoginOut: BaseOut
    {
        public string token { get; set; }
        public User user { get; set; }
    }
}
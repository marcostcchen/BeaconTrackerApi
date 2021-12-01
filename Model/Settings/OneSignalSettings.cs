namespace BeaconTrackerApi.Model.Settings
{
    public class OneSignalSettings : IOneSignalSettings
    {
        public string Auth { get; set; }
        public string AppId { get; set; }
    }

    public interface IOneSignalSettings
    {
        string Auth { get; set; }
        string AppId { get; set; }
    }
}

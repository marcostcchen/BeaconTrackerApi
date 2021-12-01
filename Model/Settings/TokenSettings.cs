namespace BeaconTrackerApi.Model.Settings
{
    public interface ITokenSettings
    {
        string Secret { get; set; }
    }
    public class TokenSettings: ITokenSettings
    {
        public string Secret { get; set; }
    }
}

using BeaconTrackerApi.Enum;

namespace BeaconTrackerApi.Model
{
    public class Region
    {
        public int idRegion { get; set; }
        public string nome { get; set; }
        public string description { get; set; }
        public DangerLevel dangerLevel { get; set; }
        public int maxStayTimeMinutes { get; set; }
        public int avgTemperature { get; set; }
    }
}
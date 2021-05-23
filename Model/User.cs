using BeaconTrackerApi.Enum;

namespace BeaconTrackerApi.Model
{
    public class User
    {
        public int idUser { get; set; }
        public string name { get; set; }
        public string login { get; set; }
        public Roles idRole { get; set; }
    }
}
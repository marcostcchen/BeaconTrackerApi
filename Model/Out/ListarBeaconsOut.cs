using System.Collections;
using System.Collections.Generic;
using BeaconTrackerApi.Enum;

namespace BeaconTrackerApi.Model
{
    public class ListarBeaconsOut: BaseOut
    {
        public IEnumerable<Beacon> listaBeacons { get; set; }
    }
}
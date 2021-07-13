using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaconTrackerApi.Model.Out
{
    public class ListarRegionsOut: BaseOut
    {
        public IEnumerable<RegionMap> listaRegionsMap { get; set; }
    }
}

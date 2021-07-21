using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaconTrackerApi.Model
{
    public class ListarUsuariosOut: BaseOut
    {
        public IEnumerable<User> listUsuarios { get; set; }
    }
}

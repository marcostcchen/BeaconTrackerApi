using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaconTrackerApi.Model
{
    public class UserWorkSession
    {
        public string name { get; set; }
        public List<WorkSession> listWorkSessions { get; set; }
    }
}

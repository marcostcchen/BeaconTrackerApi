using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaconTrackerApi.Model
{
    public class ListWorkingSessionOut: BaseOut
    {
        public List<UserWorkSession> usersWorkingSessions { get; set; }
    }
}

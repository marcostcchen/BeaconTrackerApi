using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaconTrackerApi.Model
{
    public class StartRestingIn
    {
        public string userId { get; set; }
        public int? minRestMinutes { get; set; }
    }
}

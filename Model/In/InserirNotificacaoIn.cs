using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaconTrackerApi.Model
{
    public class InserirNotificacaoIn
    {
        public string userId { get; set; }
        public string userId_OneSignal { get; set; }
        public string nome { get; set; }
        public DateTime horaEnvio { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
    }
}

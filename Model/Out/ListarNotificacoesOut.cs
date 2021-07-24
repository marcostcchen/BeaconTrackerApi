using System.Collections;
using System.Collections.Generic;
using BeaconTrackerApi.Enum;

namespace BeaconTrackerApi.Model
{
    public class ListarNotificacoesOut : BaseOut
    {
        public IEnumerable<Notification> listaNotificacoes { get; set; }
    }
}
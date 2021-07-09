using System;
using BeaconTrackerApi.Enum;
using BeaconTrackerApi.Model;
using BeaconTrackerApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeaconTrackerApi.Controllers
{
    public class UserBeaconRSSIController: ControllerBase
    {
        private readonly UserBeaconRSSIService _service;

        public UserBeaconRSSIController(UserBeaconRSSIService service)
        {
            _service = service;
        }
        
        [HttpPost]
        [AllowAnonymous]
        [Route("/api/enviar-user-beacon-RSSI")]
        public IActionResult RegistrarMedicao([FromBody] SendRSSIBeaconIn sendRSSIIn)
        {
            var sendRSSIBeaconOut = new SendRSSIBeaconOut();

            try
            {
                var userBeaconRssi = new UserBeaconRSSI()
                {
                    idUser = sendRSSIIn.idUser,
                    RSSIBeaconId1 = sendRSSIIn.RSSIBeacon1,
                    RSSIBeaconId2 = sendRSSIIn.RSSIBeacon2,
                    RSSIBeaconId3 = sendRSSIIn.RSSIBeacon3,
                    measureTime = DateTime.Now
                };
                    
                _service.InserirUserBeaconRSSI(userBeaconRssi);
                sendRSSIBeaconOut.status = Status.Sucess;
                sendRSSIBeaconOut.message = "Medicoes armazenadas com sucesso";
                return Ok(sendRSSIBeaconOut);
            }
            catch (Exception e)
            {
                sendRSSIBeaconOut.status = Status.Error;
                sendRSSIBeaconOut.message = e.Message;
                return Ok(sendRSSIBeaconOut);   
            }
        }

    }
}
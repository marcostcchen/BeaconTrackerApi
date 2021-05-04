using System;
using BeaconTrackerApi.Database;
using BeaconTrackerApi.Enum;
using BeaconTrackerApi.Model.In;
using BeaconTrackerApi.Model.Out;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeaconTrackerApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class BeaconController : ControllerBase
    {
        [HttpPost]
        [Authorize]
        [Route("/api/send-rssi-beacon")]
        public IActionResult RegistrarMedicao([FromBody] SendRSSIBeaconIn sendRSSIIn)
        {
            var sendRSSIBeaconOut = new SendRSSIBeaconOut();

            try
            {
                new BeaconDAO().InsertMeasure(sendRSSIIn);
                sendRSSIBeaconOut.status = Status.Sucess;
                sendRSSIBeaconOut.message = "Medicoes armazenadas com sucesso";
                return Ok(sendRSSIBeaconOut);
            }
            catch (Exception e)
            {
                sendRSSIBeaconOut.status = Status.Error;
                sendRSSIBeaconOut.message = e.Message;
                return BadRequest(sendRSSIBeaconOut);   
            }
        }
    }
}
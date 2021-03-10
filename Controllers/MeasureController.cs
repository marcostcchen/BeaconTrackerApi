using System;
using BeaconTrackerApi.Database;
using BeaconTrackerApi.Enum;
using BeaconTrackerApi.Model.In;
using BeaconTrackerApi.Model.Out;
using Microsoft.AspNetCore.Mvc;

namespace BeaconTrackerApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class MedicaoController : ControllerBase
    {
        [Route("/api/registrar-medicao")]
        [HttpPost]
        public IActionResult RegistrarMedicao([FromBody] MeasureIn measureIn)
        {
            var measureOut = new MeasureOut();

            try
            {
                new MeasureDAO().InsertMeasure(measureIn);
                measureOut.status = Status.Sucess;
                measureOut.message = "Medicoes armazenadas com sucesso";
                return Ok(measureOut);
            }
            catch (Exception e)
            {
                measureOut.status = Status.Error;
                measureOut.message = e.Message;
                return BadRequest(measureOut);   
            }
        }
    }
}
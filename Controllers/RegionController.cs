using System;
using BeaconTrackerApi.Enum;
using BeaconTrackerApi.Model;
using BeaconTrackerApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeaconTrackerApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class RegionController : ControllerBase
    {
        private readonly RegionService _regionService;

        public RegionController(RegionService regionService)
        {
            _regionService = regionService;
        }

        [AllowAnonymous]
        [Route("/api/atualizar-map-location")]
        [HttpPost]
        public IActionResult AtualizarMapLocation([FromBody] MapLocationIn mapLocationIn)
        {
            var mapLocationOut = new MapLocationOut();

            try
            {
                if (mapLocationIn.idRegion is null) throw new Exception("Faltam parametros! idRegion");
                if (mapLocationIn.beaconRssi is null) throw new Exception("Faltam parametros! beaconRssi");

                var idRegion = mapLocationIn.idRegion;
                var beaconRssi = mapLocationIn.beaconRssi;
                _regionService.UpdateLocationRSSI(idRegion, beaconRssi);
                
                mapLocationOut.status = Status.Sucess;
                mapLocationOut.message = "Atualizacao efetuado com sucesso!";
                return Ok(mapLocationOut);
            }
            catch (Exception e)
            {
                mapLocationOut.status = Status.Error;
                mapLocationOut.message = e.Message;
                return Ok(mapLocationOut);
            }
        }
    }
}
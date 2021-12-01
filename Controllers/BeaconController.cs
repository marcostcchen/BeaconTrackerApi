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
    public class BeaconController : ControllerBase
    {
        private readonly BeaconService _beaconService;

        public BeaconController(BeaconService beaconService)
        {
            _beaconService = beaconService;
        }
        
        [HttpGet]
        [Authorize]
        [Route("/api/listar-beacons")]
        public IActionResult ListarBeacons()
        {
            var listarBeaconsOut = new ListarBeaconsOut();

            try
            {
                var listarBeacons = _beaconService.ListarBeacons();
                
                listarBeaconsOut.status = Status.Sucess;
                listarBeaconsOut.listaBeacons = listarBeacons;
                listarBeaconsOut.message = "Listagem de beacons com sucesso!";
                return Ok(listarBeaconsOut);
            }
            catch (Exception e)
            {
                listarBeaconsOut.status = Status.Error;
                listarBeaconsOut.message = e.Message;
                return Ok(listarBeaconsOut);   
            }
        }
    }
}
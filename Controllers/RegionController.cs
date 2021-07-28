using System;
using System.Collections.Generic;
using BeaconTrackerApi.Enum;
using BeaconTrackerApi.Model;
using BeaconTrackerApi.Model.Out;
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

        [Authorize]
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


        [Authorize]
        [Route("/api/listar-regions-map")]
        [HttpPost]
        public IActionResult ListarRegions()
        {
            var listarRegionsOut = new ListarRegionsOut();

            try
            {
                var listaRegionsMap = new List<RegionMap>();
                var regions = _regionService.GetRegions();

                regions.ForEach(region =>
                {
                    var RSSIBeaconId1Avg = 0;
                    var RSSIBeaconId2Avg = 0;
                    var RSSIBeaconId3Avg = 0;

                    region.mapLocation.ForEach(map =>
                    {
                        RSSIBeaconId1Avg += map.RSSIBeaconId1;
                        RSSIBeaconId2Avg += map.RSSIBeaconId2;
                        RSSIBeaconId3Avg += map.RSSIBeaconId3;
                    });

                    RSSIBeaconId1Avg /= region.mapLocation.Count;
                    RSSIBeaconId2Avg /= region.mapLocation.Count;
                    RSSIBeaconId3Avg /= region.mapLocation.Count;

                    var regionMap = new RegionMap()
                    {
                        Id = region.Id,
                        name = region.name,
                        description = region.description,
                        dangerLevel = region.dangerLevel,
                        maxStayTimeMinutes = region.maxStayTimeMinutes,
                        avgTemperature = region.avgTemperature,
                        RSSIBeaconId1Avg = RSSIBeaconId1Avg,
                        RSSIBeaconId2Avg = RSSIBeaconId2Avg,
                        RSSIBeaconId3Avg = RSSIBeaconId3Avg,
                        idBeaconMinRSSI = region.idBeaconMinRSSI
                    };
                    listaRegionsMap.Add(regionMap);
                });

                listarRegionsOut.listaRegionsMap = listaRegionsMap;
                listarRegionsOut.status = Status.Sucess;
                listarRegionsOut.message = "Listagem de regioes com sucesso!";
                return Ok(listarRegionsOut);
            }
            catch (Exception e)
            {
                listarRegionsOut.status = Status.Error;
                listarRegionsOut.message = e.Message;
                return Ok(listarRegionsOut);
            }
        }
    }
}
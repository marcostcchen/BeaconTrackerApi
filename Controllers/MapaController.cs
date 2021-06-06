using System;
using BeaconTrackerApi.Database;
using BeaconTrackerApi.Enum;
using BeaconTrackerApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeaconTrackerApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class MapaController: ControllerBase
    {
        [HttpPost]
        [Authorize]
        [Route("/api/listar-detalhes-regiao")]
        public IActionResult ListarDetalhesRegiao([FromBody] ListarDetalhesRegiaoIn listarDetalhesRegiaoIn)
        {
            var listarDetalhesRegiaoOut = new ListarDetalhesRegiaoOut();

            try
            {
                if (listarDetalhesRegiaoIn is null) throw new Exception("Parametro json não passado!");
                if (listarDetalhesRegiaoIn.idRegion is null) throw new Exception("Parametro idRegion não passado!");

                var idRegion = (int) listarDetalhesRegiaoIn.idRegion;
                
                var region = new MapaDAO().GetRegionDetail(idRegion);
                listarDetalhesRegiaoOut.region = region;
                listarDetalhesRegiaoOut.status = Status.Sucess;
                listarDetalhesRegiaoOut.message = "Listagem de informacoes da regiao com sucesso";
                return Ok(listarDetalhesRegiaoOut);
            }
            catch (Exception e)
            {
                listarDetalhesRegiaoOut.status = Status.Error;
                listarDetalhesRegiaoOut.message = e.Message;
                return Ok(listarDetalhesRegiaoOut);   
            }
        }
    }
}
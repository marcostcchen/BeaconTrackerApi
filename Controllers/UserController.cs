using System;
using BeaconTrackerApi.Database;
using BeaconTrackerApi.Enum;
using BeaconTrackerApi.Model;
using BeaconTrackerApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeaconTrackerApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [Route("/api/efetuar-login")]
        [HttpPost]
        public IActionResult EfetuarLogin([FromBody] LoginIn loginIn)
        {
            var loginOut = new LoginOut();

            try
            {
                if (loginIn.login is null) throw new Exception("Campo login está vazio!");
                if (loginIn.password is null) throw new Exception("Campo password está vazio!");

                var listaUsuarios = _userService.GetUsers();
                var userFound = listaUsuarios.Find(u => u.login == loginIn.login && u.password == loginIn.password);
                if (userFound is null) throw new Exception("Usuário não encontrado!");
                if (userFound.active == 0) throw new Exception("Usuário inativo!");

                var userReturn = new User()
                {
                    Id = userFound.Id,
                    name = userFound.name,
                    role = userFound.role
                };

                loginOut.status = Status.Sucess;
                loginOut.token = TokenService.GenerateToken(userReturn);
                loginOut.message = "Login efetuado com sucesso";
                loginOut.user = userReturn;
                return Ok(loginOut);
            }
            catch (Exception e)
            {
                loginOut.status = Status.Error;
                loginOut.message = e.Message;
                return Ok(loginOut);
            }
        }
        
        [HttpPost]
        [AllowAnonymous]
        [Route("/api/enviar-user-beacon-RSSI")]
        public IActionResult RegistrarMedicao([FromBody] SendRSSIBeaconIn sendRSSIIn)
        {
            var sendRSSIBeaconOut = new SendRSSIBeaconOut();

            try
            {
                var userBeaconRssi = new BeaconRSSI()
                {
                    RSSIBeaconId1 = sendRSSIIn.RSSIBeaconId1,
                    RSSIBeaconId2 = sendRSSIIn.RSSIBeaconId2,
                    RSSIBeaconId3 = sendRSSIIn.RSSIBeaconId3,
                    measureTime = DateTime.Now,
                    regionName = sendRSSIIn.regionName
                };
                    
                _userService.UpdateUserRSSI(sendRSSIIn.idUser, userBeaconRssi);
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
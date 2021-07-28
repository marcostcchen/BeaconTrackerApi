using System;
using System.Linq;
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

                if(userFound.role == Roles.Funcionario) _userService.UpdateUserIdOneSignal(userFound.Id, loginIn.userId_OneSignal);

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
        [Authorize]
        [Route("/api/start-working")]
        public IActionResult StartWorking([FromBody] StartWorkingIn startWorkingIn)
        {
            var startWorkingOut = new BaseOut();

            try
            {
                if (startWorkingIn is null) throw new Exception("Faltam parametros");
                if (startWorkingIn.maxStayMinutes is null) throw new Exception("Faltam parametros");
                if (startWorkingIn.userId is null) throw new Exception("Faltam parametros");

                var userId = startWorkingIn.userId;
                var maxStayMinutes = (int)startWorkingIn.maxStayMinutes;

                _userService.UpdateUserStartWorking(userId, maxStayMinutes);
                startWorkingOut.status = Status.Sucess;
                startWorkingOut.message = "Usuário iniciado com sucesso!";
                return Ok(startWorkingOut);
            }
            catch (Exception e)
            {
                startWorkingOut.status = Status.Error;
                startWorkingOut.message = e.Message;
                return Ok(startWorkingOut);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("/api/start-resting")]
        public IActionResult StartResting([FromBody] StartRestingIn startRestingIn)
        {
            var startRestingOut = new BaseOut();

            try
            {
                if (startRestingIn is null) throw new Exception("Faltam parametros");
                if (startRestingIn.minRestMinutes is null) throw new Exception("Faltam parametros");
                if (startRestingIn.userId is null) throw new Exception("Faltam parametros");

                var userId = startRestingIn.userId;
                var maxStayMinutes = (int)startRestingIn.minRestMinutes;

                _userService.UpdateUserStartResting(userId, maxStayMinutes);
                startRestingOut.status = Status.Sucess;
                startRestingOut.message = "Usuário pausado com sucesso!";
                return Ok(startRestingOut);
            }
            catch (Exception e)
            {
                startRestingOut.status = Status.Error;
                startRestingOut.message = e.Message;
                return Ok(startRestingOut);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("/api/finish-working")]
        public IActionResult FinishWorking([FromBody] FinishWorkingIn finishWorkingIn)
        {
            var finishWorkingOut = new BaseOut();

            try
            {
                if (finishWorkingIn is null) throw new Exception("Faltam parametros");
                if (finishWorkingIn.userId is null) throw new Exception("Faltam parametros");

                var userId = finishWorkingIn.userId;
                _userService.UpdateUserFinishWorking(userId);
                finishWorkingOut.status = Status.Sucess;
                finishWorkingOut.message = "Usuário finalizado com sucesso!";
                return Ok(finishWorkingOut);
            }
            catch (Exception e)
            {
                finishWorkingOut.status = Status.Error;
                finishWorkingOut.message = e.Message;
                return Ok(finishWorkingOut);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("/api/list-working-sessions")]
        public IActionResult ListWorkingSessions()
        {
            var listWorkingSessionsOut = new ListWorkingSessionOut();

            try
            {
                var usersWorkingSessions = _userService.GetWorkingSessions();
                listWorkingSessionsOut.status = Status.Sucess;
                listWorkingSessionsOut.usersWorkingSessions = usersWorkingSessions;
                listWorkingSessionsOut.message = "Listagem de sessoes com sucesso!";
                return Ok(listWorkingSessionsOut);
            }
            catch (Exception e)
            {
                listWorkingSessionsOut.status = Status.Error;
                listWorkingSessionsOut.message = e.Message;
                return Ok(listWorkingSessionsOut);
            }
        }

        [HttpPost]
        [Authorize]
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

        [HttpPost]
        [Authorize]
        [Route("/api/listar-ultimas-localizacoes")]
        public IActionResult ListarUltimasLocalizacoes()
        {
            var listarUsuariosOut = new ListarUsuariosOut();

            try
            {
                var users = _userService.GetUsers();

                users.ForEach(user =>
                {
                    user.password = null;
                    user.active = null;
                    user.login = null;
                    user.role = null;
                    user.beaconsRSSI = null;
                });

                users = users.OrderByDescending(user => user.lastLocation.regionName).ToList();

                listarUsuariosOut.listUsuarios = users;
                listarUsuariosOut.status = Status.Sucess;
                listarUsuariosOut.message = "Listagem de informacoes da regiao com sucesso";
                return Ok(listarUsuariosOut);
            }
            catch (Exception e)
            {
                listarUsuariosOut.status = Status.Error;
                listarUsuariosOut.message = e.Message;
                return Ok(listarUsuariosOut);
            }
        }
    }
}
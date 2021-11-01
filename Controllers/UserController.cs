using System;
using System.Linq;
using System.Security.Claims;
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
        [Route("/api/create-work-session")]
        public IActionResult StartWorking([FromBody] CreateWorkSessionIn createWorkSessionIn)
        {
            var startWorkingOut = new BaseOut();

            try
            {
                if (createWorkSessionIn is null) throw new Exception("Faltam parametros");

                var userId = createWorkSessionIn.userId;
                var workSession = createWorkSessionIn.workSession;

                _userService.CreateWorkSession(userId, workSession);
                startWorkingOut.status = Status.Sucess;
                startWorkingOut.message = "Work Session criado com sucesso!";
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
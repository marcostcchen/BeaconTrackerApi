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
    public class LoginController : ControllerBase
    {
        [AllowAnonymous]
        [Route("/api/efetuar-login")]
        [HttpPost]
        public IActionResult EfetuarLogin([FromBody] LoginIn loginIn)
        {
            var loginOut = new LoginOut();

            try
            {
                var user = new LoginDAO().GetUsers(loginIn.login, loginIn.password);
                
                loginOut.status = Status.Sucess;
                loginOut.token = TokenService.GenerateToken(user);
                loginOut.message = "Login efetuado com sucesso";
                loginOut.user = user;
                return Ok(loginOut);
            }
            catch (Exception e)
            {
                loginOut.status = Status.Error;
                loginOut.message = e.Message;
                return Ok(loginOut);
            }
        }
    }
}
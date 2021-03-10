﻿using System;
using System.Collections.Generic;
using System.Linq;
using BeaconTrackerApi.Database;
using BeaconTrackerApi.Enum;
using BeaconTrackerApi.Model;
using BeaconTrackerApi.Model.In;
using BeaconTrackerApi.Model.Out;
using Microsoft.AspNetCore.Mvc;

namespace BeaconTrackerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] LoginIn loginIn)
        {
            var loginOut = new LoginOut();

            try
            {
                var user = new LoginDao().GetUsers(loginIn.login, loginIn.password);

                loginOut.status = Status.Sucess;
                loginOut.message = "sucesso";
                loginOut.user = user;
                return Ok(loginOut);
            }
            catch (Exception e)
            {
                loginOut.status = Status.Error;
                loginOut.message = "Error";
                return Ok(loginOut);
            }
        }
    }
}
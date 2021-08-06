using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using BeaconTrackerApi.Database;
using BeaconTrackerApi.Enum;
using BeaconTrackerApi.Model;
using BeaconTrackerApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BeaconTrackerApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        [Authorize]
        [Route("/api/listar-notificacoes")]
        public IActionResult ListarNotificacoes()
        {
            var listarNotificacoesOut = new ListarNotificacoesOut();

            try
            {
                var listarNotificacoes = _notificationService.GetNotifications();

                listarNotificacoesOut.status = Status.Sucess;
                listarNotificacoesOut.listaNotificacoes = listarNotificacoes;
                listarNotificacoesOut.message = "Listagem de notificações com sucesso!";
                return Ok(listarNotificacoesOut);
            }
            catch (Exception e)
            {
                listarNotificacoesOut.status = Status.Error;
                listarNotificacoesOut.message = e.Message;
                return Ok(listarNotificacoesOut);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("/api/enviar-notificacao")]
        public IActionResult InserirNotificacao(InserirNotificacaoIn inserirNotificacaoIn)
        {
            var inserirNotificacaoOut = new BaseOut();

            try
            {
                var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;
                request.KeepAlive = true;
                request.Method = "POST";
                request.ContentType = "application/json; charset=utf-8";
                request.Headers.Add("authorization", "Basic YmM4ZDVjZDAtOTM1YS00ZjJlLWI3OTMtZjUyMWYxNjcxMWRm");

                var obj = new
                {
                    app_id = "b3e53c7e-4779-4ec1-b23c-d2ebd098a66b",
                    include_player_ids = new string[] { inserirNotificacaoIn.userId_OneSignal },
                    data = new { description = "Enviar Notificacao" },
                    headings = new { en = inserirNotificacaoIn.titulo, pt = inserirNotificacaoIn.titulo },
                    contents = new { en = inserirNotificacaoIn.descricao, pt = inserirNotificacaoIn.descricao },
                    small_icon = "ic_stat_onesignal_default",
                    large_icon = "ic_stat_onesignal_default"
                };

                var param = JsonSerializer.Serialize(obj);
                byte[] byteArray = Encoding.UTF8.GetBytes(param);

                string responseContent = null;

                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }

                if (responseContent is null) throw new Exception("Não foi possível enviar a notificação!");

                var notification = new Notification()
                {
                    userId_OneSignal = inserirNotificacaoIn.userId_OneSignal,
                    nome = inserirNotificacaoIn.nome,
                    horaEnvio = inserirNotificacaoIn.horaEnvio,
                    titulo = inserirNotificacaoIn.titulo,
                    descricao = inserirNotificacaoIn.descricao
                };

                _notificationService.InsertNotification(notification);

                inserirNotificacaoOut.status = Status.Sucess;
                inserirNotificacaoOut.message = "Notificação inserida com sucesso!";
                return Ok(inserirNotificacaoOut);
            }
            catch (Exception e)
            {
                inserirNotificacaoOut.status = Status.Error;
                inserirNotificacaoOut.message = e.Message;
                return Ok(inserirNotificacaoOut);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("/api/listar-notificacoes-usuario")]
        public IActionResult ListarNotificacoesUsuarios(string name)
        {
            var listarNotificacoesOut = new ListarNotificacoesOut();

            try
            {
                var notifications = _notificationService.GetNotifications().Where(notif => notif.nome == name);
                listarNotificacoesOut.listaNotificacoes = notifications;
                return Ok(listarNotificacoesOut);
            }
            catch (Exception e)
            {
                listarNotificacoesOut.status = Status.Error;
                listarNotificacoesOut.message = e.Message;
                return Ok(listarNotificacoesOut);
            }
        }
    }
}
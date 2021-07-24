using System;
using BeaconTrackerApi.Database;
using BeaconTrackerApi.Enum;
using BeaconTrackerApi.Model;
using BeaconTrackerApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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
        [AllowAnonymous]
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
        [AllowAnonymous]
        [Route("/api/inserir-notificacao")]
        public IActionResult InserirNotificacao(InserirNotificacaoIn inserirNotificacaoIn)
        {
            var inserirNotificacaoOut = new BaseOut();

            try
            {
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
    }
}
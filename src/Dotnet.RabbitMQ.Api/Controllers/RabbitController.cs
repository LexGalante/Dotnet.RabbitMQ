using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnet.RabbitMQ.Api.ViewModels;
using Dotnet.RabbitMQ.Infra;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dotnet.RabbitMQ.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitController : ControllerBase
    {
        private readonly IService _service;

        public RabbitController(IService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("SendMessage")]
        public ActionResult SendMessage([FromBody] SendMessageViewModel viewModel)
        {
            _service.SendMessage(new Queue() { Name = viewModel.Queue, Durable = true }, new Message() { Content = viewModel.Message });

            return Ok("Mensagem enviada com sucesso");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.RabbitMQ.Api.ViewModels
{
    public class SendMessageViewModel
    {
        public string Queue { get; set; }
        public string Message { get; set; }
    }
}

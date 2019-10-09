using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.RabbitMQ.Infra
{
    public class Message
    {
        public string Content { get; set; }

        public byte[] GetContentBytes() => Encoding.UTF8.GetBytes(Content);
    }
}

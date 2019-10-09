using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.RabbitMQ.Infra
{
    public class Queue
    {
        public string Exchange { get; set; } = string.Empty;
        public string Name { get; set; }
        public bool Durable { get; set; }
        public bool Exclusive { get; set; }
        public bool AutoDelete { get; set; }
        public IDictionary<string, object> Arguments { get; set; }
        public bool AutoAck { get; set; }
    }
}

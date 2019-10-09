using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.RabbitMQ.Infra
{
    public interface IService
    {
        void SendMessage(Queue queue, Message message);
        string GetMessage(Queue queue);
    }
}

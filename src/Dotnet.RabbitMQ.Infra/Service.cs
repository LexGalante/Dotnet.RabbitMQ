using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using RabbitMQ.Client.Exceptions;

namespace Dotnet.RabbitMQ.Infra
{
    public class Service : IService
    {
        private readonly Settings _settings;

        public Service(IOptions<Settings> options)
        {
            _settings = options.Value;
        }

        private IConnection GetConnection()
        {
            try
            {
                return new ConnectionFactory()
                {
                    HostName = _settings.HostName,
                    Port = _settings.Port,
                    UserName = _settings.UserName,
                    Password = _settings.Password
                }.CreateConnection();
            }
            catch (BrokerUnreachableException)
            {
                Task.Delay(TimeSpan.FromSeconds(5));
                return new ConnectionFactory()
                {
                    HostName = _settings.HostName,
                    Port = _settings.Port,
                    UserName = _settings.UserName,
                    Password = _settings.Password
                }.CreateConnection();
            }
        }

        public void SendMessage(Queue queue, Message message)
        {
            using (var connection = GetConnection())
            {
                using var model = connection.CreateModel();
                model.QueueDeclare(queue: queue.Name,
                                   durable: queue.Durable,
                                   exclusive: queue.Exclusive,
                                   autoDelete: queue.AutoDelete,
                                   arguments: queue.Arguments);

                model.BasicPublish(exchange: queue.Exchange,
                                   routingKey: queue.Name,
                                   basicProperties: null,
                                   body: message.GetContentBytes());
            }
        }

        public string GetMessage(Queue queue)
        {
            using (var connection = GetConnection())
            {
                using var model = connection.CreateModel();
                model.QueueDeclare(queue: queue.Name,
                                   durable: queue.Durable,
                                   exclusive: queue.Exclusive,
                                   autoDelete: queue.AutoDelete,
                                   arguments: queue.Arguments);

                var result = model.BasicGet(queue: queue.Name, queue.AutoAck);

                if (result is null)
                    return string.Empty;
                else
                {
                    var props = result.BasicProperties;
                    byte[] body = result.Body;

                    return Encoding.UTF8.GetString(body);
                }
            }
        }
    }
}

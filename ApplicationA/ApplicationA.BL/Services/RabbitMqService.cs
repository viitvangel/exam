using ApplicationA.BL.Interfaces;
using ApplicationA.Models;
using MessagePack;
using RabbitMQ.Client;
using System;
using System.Threading.Tasks;

namespace ApplicationA.BL.Services
{
    public class RabbitMqService : IRabbitMqService, IDisposable
    {
        private readonly IConnection connection;
        private readonly IModel model;

        public RabbitMqService()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
            };

            connection = factory.CreateConnection();
            model = connection.CreateModel();
            model.ExchangeDeclare("Test", ExchangeType.Fanout);
            model.QueueDeclare("autopart_queue", true, false);
        }

        public void Dispose()
        {
            model?.Dispose();
            connection?.Dispose();
        }

        public async Task PublishAutopartAsync(Autopart autopart)
        {
            await Task.Factory.StartNew(() =>
            {
                var body = MessagePackSerializer.Serialize(autopart);
                model.BasicPublish("", "autopart_queue", body: body);
            });
        }
    }
}

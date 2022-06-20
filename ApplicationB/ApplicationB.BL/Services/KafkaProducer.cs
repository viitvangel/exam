using ApplicationB.BL.Interfaces;
using ApplicationB.Models;
using Confluent.Kafka;
using System.Threading.Tasks;

namespace ApplicationB.BL.Services
{
    public class KafkaProducer : IKafkaProducer
    {
        private IProducer<int, Autopart> _producer;

        public KafkaProducer()
        {
            var config = new ProducerConfig()
            {
                BootstrapServers = "localhost:9092"
            };

            _producer = new ProducerBuilder<int, Autopart>(config)
                            .SetValueSerializer(new MsgPackSerializer<Autopart>())
                            .Build();
        }
        public async Task ProduceAutopart(Autopart autopart)
        {
            await _producer.ProduceAsync("Autopart", new Message<int, Autopart>()
            {
                Key = autopart.Id,
                Value = autopart
            });
        }
    }
}

using ApplicationA.BL.Interfaces;
using ApplicationA.DL;
using ApplicationA.Models;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationA.BL.Services
{
    public class KafkaConsumer : IHostedService
    {
        private readonly IAutopartService autopartService;
        private IConsumer<byte[], Autopart> consumer;

        public KafkaConsumer(IAutopartService autopartService)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost",
                AutoCommitIntervalMs = 5000,
                FetchWaitMaxMs = 50,
                GroupId = Guid.NewGuid().ToString(),
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = true,
                ClientId = "2"
            };

            consumer = new ConsumerBuilder<byte[], Autopart>(config)
                            .SetValueDeserializer(new MsgPackDeserializer<Autopart>())
                            .Build();
            this.autopartService = autopartService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            consumer.Subscribe("Cars");
            Task.Factory.StartNew(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                { 
                    try
                    {
                        var result = consumer.Consume(cancellationToken);
                        autopartService.Create(result.Message.Value);
                        Console.WriteLine("Incoming message:");
                        Console.WriteLine($"Autopart: {result.Message.Value.AutopartName}, Category Name: {result.Message.Value.CategoryName} Car Brand: {result.Message.Value.CarBrand}, Car Model: {result.Message.Value.Model} {Environment.NewLine}");
                    }
                    catch (ConsumeException ex)
                    {
                        Console.WriteLine($"Error: {ex.Error.Reason}");
                    }
                }
            }, cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

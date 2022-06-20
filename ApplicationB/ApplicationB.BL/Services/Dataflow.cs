using ApplicationB.BL.Interfaces;
using ApplicationB.Models;
using MessagePack;
using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ApplicationB.BL.Services
{
    public class Dataflow : IDataflow
    {
        private IKafkaProducer _producer;
        TransformBlock<byte[], Autopart> entryBlock = new TransformBlock<byte[], Autopart>(data => MessagePackSerializer.Deserialize<Autopart>(data));

        public Dataflow(IKafkaProducer producer)
        {
            _producer = producer;

            var enrichBlock = new TransformBlock<Autopart, Autopart>(a =>
            {
                Console.WriteLine($"Received value: {a.CategoryName}");
                a.CategoryName += " change";
                return a;
            });

            var publishBlock = new ActionBlock<Autopart>(a =>
            {
                Console.WriteLine($"Updated value: {a.CategoryName} \n");
                _producer.ProduceAutopart(a);
            });

            var linkOptions = new DataflowLinkOptions()
            {
                PropagateCompletion = true
            };

            entryBlock.LinkTo(enrichBlock, linkOptions);
            enrichBlock.LinkTo(publishBlock, linkOptions);

        }
        public async Task SendAutopart(byte[] data)
        {          
            await entryBlock.SendAsync(data);
        }  
    }
}

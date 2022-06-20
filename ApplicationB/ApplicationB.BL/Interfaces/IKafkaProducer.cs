using ApplicationB.Models;
using System.Threading.Tasks;

namespace ApplicationB.BL.Interfaces
{
    public interface IKafkaProducer
    {
        Task ProduceAutopart(Autopart autpart);
    }
}

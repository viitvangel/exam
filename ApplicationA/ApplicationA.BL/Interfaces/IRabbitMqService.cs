using ApplicationA.Models;
using System.Threading.Tasks;

namespace ApplicationA.BL.Interfaces
{
    public interface IRabbitMqService
    {
        Task PublishAutopartAsync(Autopart car);
    }
}

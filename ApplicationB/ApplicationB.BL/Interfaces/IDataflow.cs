using System.Threading.Tasks;

namespace ApplicationB.BL.Interfaces
{
    public interface IDataflow
    {
        Task SendAutopart(byte[] data);
    }
}

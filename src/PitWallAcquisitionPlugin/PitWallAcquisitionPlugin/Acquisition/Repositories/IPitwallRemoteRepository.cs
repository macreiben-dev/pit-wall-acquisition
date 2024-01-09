using System.Threading.Tasks;

namespace PitWallAcquisitionPlugin.Aggregations.Telemetries.Repositories
{
    public interface IPitwallRemoteRepository
    {
        Task SendAsync(object dataToSend);

        string Uri { get; }
    }
}
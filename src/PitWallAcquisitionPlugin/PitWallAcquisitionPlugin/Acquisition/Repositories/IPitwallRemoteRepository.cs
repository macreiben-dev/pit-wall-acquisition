using System.Threading.Tasks;

namespace PitWallAcquisitionPlugin.Aggregations.Telemetries.Repositories
{
    internal interface IPitwallRemoteRepository
    {
        Task SendAsync(object dataToSend);

        string Uri { get; }
    }
}
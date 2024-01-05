using System.Threading.Tasks;

namespace PitWallAcquisitionPlugin.Aggregations.Telemetries.Repositories
{
    public interface IStagingTelemetryDataRepository
    {
        Task SendAsync(object dataToSend);
    }
}
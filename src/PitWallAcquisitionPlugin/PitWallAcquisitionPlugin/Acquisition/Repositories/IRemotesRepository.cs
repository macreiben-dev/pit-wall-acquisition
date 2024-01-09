using PitWallAcquisitionPlugin.Aggregations.Telemetries.Repositories;

namespace PitWallAcquisitionPlugin.Acquisition.Repositories
{
    public interface IRemotesRepository
    {
        IPitwallRemoteRepository SelectFrom(RemoteTypeEnum remoteType);
    }
}
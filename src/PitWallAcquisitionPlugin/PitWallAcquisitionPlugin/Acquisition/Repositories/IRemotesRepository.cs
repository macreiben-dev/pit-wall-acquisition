using PitWallAcquisitionPlugin.Aggregations.Telemetries.Repositories;

namespace PitWallAcquisitionPlugin.Acquisition.Repositories
{
    internal interface IRemotesRepository
    {
        IPitwallRemoteRepository SelectFrom(RemoteTypeEnum remoteType);
    }
}
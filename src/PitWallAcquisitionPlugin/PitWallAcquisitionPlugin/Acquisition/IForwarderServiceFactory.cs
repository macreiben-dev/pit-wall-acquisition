using PitWallAcquisitionPlugin.Acquisition.Repositories;
using PitWallAcquisitionPlugin.Aggregations.Leadeboards;

namespace PitWallAcquisitionPlugin.Acquisition
{
    internal interface IForwarderServiceFactory
    {
        IDataForwarderService GetInstance(IAggregator aggregator, RemoteTypeEnum remoteType);
    }
}
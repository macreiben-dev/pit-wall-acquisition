using PitWallAcquisitionPlugin.Acquisition.Repositories;

namespace PitWallAcquisitionPlugin.Acquisition
{
    internal interface IForwarderServiceFactory
    {
        IDataForwarderService GetInstance(IAggregator aggregator, RemoteTypeEnum remoteType);
    }
}
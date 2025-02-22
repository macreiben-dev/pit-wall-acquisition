﻿using PitWallAcquisitionPlugin.Acquisition.Repositories;
using PitWallAcquisitionPlugin.Logging;

namespace PitWallAcquisitionPlugin.Acquisition
{
    internal sealed class ForwarderServiceFactory : IForwarderServiceFactory
    {
        private readonly ILogger logger;
        private readonly IRemotesRepository remotesRepository;

        public ForwarderServiceFactory(ILogger logger, IRemotesRepository remotesRepository)
        {
            this.logger = logger;
            this.remotesRepository = remotesRepository;
        }

        public IDataForwarderService GetInstance(
            IAggregator aggregator,
            RemoteTypeEnum remoteType)

        {
            return new WebApiForwarderService(aggregator,
                remotesRepository,
                logger,
                remoteType,
                1,
                5000);
        }
    }
}

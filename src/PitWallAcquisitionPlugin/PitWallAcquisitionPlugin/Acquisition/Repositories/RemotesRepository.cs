using PitWallAcquisitionPlugin.UI.ViewModels;
using System;
using System.Collections.Generic;

namespace PitWallAcquisitionPlugin.Acquisition.Repositories
{
    internal sealed class RemotesRepository : IRemotesRepository
    {
        private readonly IPitWallConfiguration configuration;
        private readonly IDictionary<RemoteTypeEnum, IPitwallRemoteRepository> remoteRepositories
            = new Dictionary<RemoteTypeEnum, IPitwallRemoteRepository>();

        public RemotesRepository(IPitWallConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IPitwallRemoteRepository SelectFrom(RemoteTypeEnum remoteType)
        {
            if (remoteType == RemoteTypeEnum.None)
            {
                throw new ArgumentException("Specify a valid remote.");
            }

            if (remoteRepositories.TryGetValue(remoteType, out IPitwallRemoteRepository remoteRepository))
            {
                return remoteRepository;
            }

            PitwallRemoteRepository repo = null;
            switch (remoteType)
            {
                case RemoteTypeEnum.Telemetry:
                    repo = new PitwallRemoteRepository(configuration, "/api/v1/telemetry");
                    break;
                case RemoteTypeEnum.Leaderboard:
                    repo = new PitwallRemoteRepository(configuration, "/api/v1/leaderboard");
                    break;
            }

            remoteRepositories[remoteType] = repo;

            return remoteRepositories[remoteType];
        }
    }
}

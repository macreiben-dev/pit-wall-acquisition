using PitWallAcquisitionPlugin.Acquisition;
using PitWallAcquisitionPlugin.Acquisition.Repositories;
using System.Collections.Generic;
using PitWallAcquisitionPlugin.Acquisition.Leadeboards;
using PitWallAcquisitionPlugin.Acquisition.Telemetries.Aggregators;

namespace PitWallAcquisitionPlugin
{
    internal sealed class AcquisitionService : IAcquisitionService
    {
        private readonly List<IDataForwarderService> _services;

        public AcquisitionService(IForwarderServiceFactory serviceFactory,
            ITelemetryLiveAggregator telemetryAggregator,
            ILeaderboardLiveAggregator leaderboardAggregator)
        {
            var services = new List<IDataForwarderService>()
            {
                serviceFactory.GetInstance(telemetryAggregator, RemoteTypeEnum.Telemetry),
                serviceFactory.GetInstance(leaderboardAggregator, RemoteTypeEnum.Leaderboard),
            };

            _services = services;
        }

        public void Start()
        {
            foreach (var service in _services)
            {
                service.Start();
            }
        }

        public void HandleDataUpdate(IPluginRecordRepository pluginRecordRepository)
        {
            foreach (var service in _services)
            {
                service.HandleDataUpdate(pluginRecordRepository);
            }
        }

        public void Stop()
        {
            foreach (var service in _services)
            {
                service.Stop();
            }
        }
    }
}

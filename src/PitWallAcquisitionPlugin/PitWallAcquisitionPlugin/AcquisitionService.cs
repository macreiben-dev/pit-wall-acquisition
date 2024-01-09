using FuelAssistantMobile.DataGathering.SimhubPlugin;
using PitWallAcquisitionPlugin.Acquisition;
using PitWallAcquisitionPlugin.Acquisition.Repositories;
using PitWallAcquisitionPlugin.Aggregations.Leadeboards;
using PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators;
using System.Collections.Generic;

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

using FuelAssistantMobile.DataGathering.SimhubPlugin;
using FuelAssistantMobile.DataGathering.SimhubPlugin.Logging;
using NFluent;
using NSubstitute;
using PitWallAcquisitionPlugin.Acquisition.Repositories;
using PitWallAcquisitionPlugin.Aggregations.Leadeboards;
using PitWallAcquisitionPlugin.Aggregations.Telemetries;
using PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators;
using PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators.Models;
using PitWallAcquisitionPlugin.Aggregations.Telemetries.Repositories;
using System.Threading;
using Xunit;

namespace PitWallAcquisitionPlugin.Tests
{
    public class WebApiForwarderServiceTest
    {
        private IAggregator _aggregator;
        private ILogger _logger;
        private IRemotesRepository _remotesRepository;
        private IPitwallRemoteRepository _repo;
        private IPluginRecordRepository _pluginRecordRepo;

        public WebApiForwarderServiceTest()
        {
            _aggregator = Substitute.For<IAggregator>();
            _logger = Substitute.For<ILogger>();

            _remotesRepository = Substitute.For<IRemotesRepository>();
            _repo = Substitute.For<IPitwallRemoteRepository>();

            _pluginRecordRepo = Substitute.For<IPluginRecordRepository>();

            _remotesRepository.SelectFrom(RemoteTypeEnum.Telemetry)
                .Returns(_repo);
        }

        private WebApiForwarderService GetTarget()
        {
            return new WebApiForwarderService(
                _aggregator,
                _remotesRepository,
                _logger,
                RemoteTypeEnum.Telemetry,
                1000,
                1);
        }

        private WebApiForwarderService GetTarget(RemoteTypeEnum remoteType)
        {
            return new WebApiForwarderService(
                _aggregator,
                _remotesRepository,
                _logger,
                remoteType,
                1000,
                1);
        }

        [Fact]
        public void Should_build()
        {
            Check.ThatCode(() => new WebApiForwarderService(
                _aggregator,
                _remotesRepository,
                _logger,
                RemoteTypeEnum.Telemetry,
                1,
                1))
                .DoesNotThrow();
        }

        [Fact]
        public void Should_start_and_isGameRunning_and_notAlready_started()
        {
            TelemetryData original = new TelemetryData()
            {
                PilotName = "SomePilot",
                SimerKey = "somekey",
                SessionTimeLeft = "00:00:01"
            };

            _aggregator.AsData()
                .Returns(original);

            _aggregator.IsDirty.Returns(true);
            
            // ACT
            var target = GetTarget();

            target.Start();

            Thread.Sleep(200);

            target.Stop();

            // ASSERT
            _repo.Received().SendAsync(
                Arg.Is<object>(c => ((ITelemetryData)c).SessionTimeLeft == "00:00:01"));
        }

        [Fact]
        public void Should_not_send_when_aggregator_not_dirty()
        {
            TelemetryData original = new TelemetryData()
            {
                SessionTimeLeft = "00:00:01"
            };

            _aggregator.AsData()
                .Returns(original);

            _aggregator.IsDirty.Returns(false);

            // ACT
            var target = GetTarget();

            target.Start();

            Thread.Sleep(200);

            target.Stop();

            // ASSERT
            _repo.Received(0).SendAsync(Arg.Any<DataVessel>());
        }

        [Theory]
        [InlineData(RemoteTypeEnum.Telemetry)]
        [InlineData(RemoteTypeEnum.Leaderboard)]
        internal void GIVEN_name_WHEN_starting_THEN_use_remoteType_in_logger(RemoteTypeEnum remoteType)
        {
            var target = GetTarget(remoteType);

            target.Start();

            Thread.Sleep(200);

            _logger.Received(1).Info($"Pitwall acquisition plugin - {remoteType} Gathering STARTED");

            target.Stop();
        }

        [Theory]
        [InlineData(RemoteTypeEnum.Telemetry)]
        [InlineData(RemoteTypeEnum.Leaderboard)]
        internal void GIVEN_name_WHEN_stopping_THEN_use_remoteType_in_logger(RemoteTypeEnum remoteType)
        {
            var target = GetTarget(remoteType);

            target.Start();

            target.Stop();

            Thread.Sleep(200);

            _logger.Received(1).Info($"Pitwall acquisition plugin - {remoteType} Gathering STOPPED");
        }

        [Fact]
        public void GIVEN_firstLaunch_AND_game_notRunning_AND_service_notStarted_THEN_stop()
        {
            _pluginRecordRepo.IsGameRunning.Returns(false);

            var target = GetTarget(RemoteTypeEnum.Telemetry);

            target.HandleDataUpdate(_pluginRecordRepo);

            _logger.Received(1).Info($"Pitwall acquisition plugin - Telemetry Gathering STOPPED");
        }

        [Fact]
        public void GIVEN_firstLaunch_AND_game_isRunning_AND_service_notStarted_THEN_start_AND_updateAggregator()
        {
            _pluginRecordRepo.IsGameRunning.Returns(true);
            _pluginRecordRepo.AirTemperature.Returns(12.3);

            var target = GetTarget(RemoteTypeEnum.Telemetry);

            target.HandleDataUpdate(_pluginRecordRepo);

            _aggregator.Received(1).UpdateAggregator(
                Arg.Is<IPluginRecordRepository>(arg => arg.AirTemperature == 12.3));
            _logger.Received(1).Info($"Pitwall acquisition plugin - Telemetry Gathering STARTED");
        }

        [Fact]
        public void GIVEN_startService_THEN_aggregator_is_cleared()
        {
            var target = GetTarget(RemoteTypeEnum.Telemetry);

            target.Start();

            _logger.Received(1).Info($"Pitwall acquisition plugin - Telemetry Gathering STARTED");
            _aggregator.Received(1).Clear();
        }

        [Fact]
        public void GIVEN_game_notRunning_AND_service_started_THEN_stop_AND_clearAggregator()
        {
            _pluginRecordRepo.IsGameRunning.Returns(true);
            _pluginRecordRepo.AirTemperature.Returns(12.3);

            var target = GetTarget(RemoteTypeEnum.Telemetry);

            target.Start();

            target.Stop();

            _logger.Received(1).Info($"Pitwall acquisition plugin - Telemetry Gathering STOPPED");
            _aggregator.Received(2).Clear();
        }

        [Fact]
        public void GIVEN_game_isRunning_AND_service_isStarted_WHEN_handleDataUpdate_called_twice_THEN_updateLiveAggregator_AND_logged_started_once()
        {
            _pluginRecordRepo.IsGameRunning.Returns(true);
            _pluginRecordRepo.AirTemperature.Returns(12.3);

            var otherRepo = Substitute.For<IPluginRecordRepository>();
            otherRepo.AirTemperature.Returns(15.6);
            otherRepo.IsGameRunning.Returns(true);

            var target = GetTarget(RemoteTypeEnum.Telemetry);

            target.Start();

            target.HandleDataUpdate(_pluginRecordRepo);
            target.HandleDataUpdate(otherRepo);

            _aggregator.Received(1).UpdateAggregator(
                Arg.Is<IPluginRecordRepository>(arg => arg.AirTemperature == 15.6));
            _logger.Received(1).Info($"Pitwall acquisition plugin - Telemetry Gathering STARTED");
        }
    }
}

using FuelAssistantMobile.DataGathering.SimhubPlugin;
using FuelAssistantMobile.DataGathering.SimhubPlugin.Logging;
using NFluent;
using NSubstitute;
using PitWallAcquisitionPlugin.Acquisition.Repositories;
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
        private ITelemetryLiveAggregator _aggregator;
        private ILogger _logger;
        private IRemotesRepository _remotesRepository;
        private IPitwallRemoteRepository _repo;

        public WebApiForwarderServiceTest()
        {
            _aggregator = Substitute.For<ITelemetryLiveAggregator>();
            _logger = Substitute.For<ILogger>();

            _remotesRepository = Substitute.For<IRemotesRepository>();
            _repo = Substitute.For<IPitwallRemoteRepository>();

            _remotesRepository.SelectFrom(RemoteTypeEnum.Telemetry)
                .Returns(_repo);
        }

        private WebApiTelemetryForwarderService GetTarget()
        {
            return new WebApiTelemetryForwarderService(
                _aggregator,
                _logger,
                1000,
                1,
                _remotesRepository);
        }

        [Fact]
        public void Should_build()
        {
            Check.ThatCode(() => new WebApiTelemetryForwarderService(
                _aggregator,
                _logger,
                1,
                1,
                _remotesRepository))
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
    }
}

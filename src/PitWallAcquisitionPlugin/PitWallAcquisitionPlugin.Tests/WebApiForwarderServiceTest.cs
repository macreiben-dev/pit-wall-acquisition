using FuelAssistantMobile.DataGathering.SimhubPlugin;
using FuelAssistantMobile.DataGathering.SimhubPlugin.Aggregations;
using FuelAssistantMobile.DataGathering.SimhubPlugin.Logging;
using FuelAssistantMobile.DataGathering.SimhubPlugin.Repositories;
using NFluent;
using NSubstitute;
using PitWallAcquisitionPlugin.Aggregations;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PitWallAcquisitionPlugin.Tests
{
    public class WebApiForwarderServiceTest
    {
        private ILiveAggregator _aggregator;
        private IStagingDataRepository _dataRepository;
        private ILogger _logger;

        public WebApiForwarderServiceTest()
        {
            _aggregator = Substitute.For<ILiveAggregator>();
            _dataRepository = Substitute.For<IStagingDataRepository>();
            _logger = Substitute.For<ILogger>();
        }

        [Fact]
        public void Should_build()
        {
            Check.ThatCode(() => new WebApiForwarderService(
                _aggregator,
                _dataRepository,
                _logger,
                1,
                1))
                .DoesNotThrow();
        }

        [Fact]
        public void Should_start_and_isGameRunning_and_notAlready_started()
        {
            Data original = new Data()
            {
                SessionTimeLeft = "00:00:01"
            };

            _aggregator.AsData()
                .Returns(original);

            _aggregator.IsDirty.Returns(true);

            var target = new WebApiForwarderService(
                _aggregator,
                _dataRepository,
                _logger,
                1000,
                1);

            target.Start();

            Thread.Sleep(200);

            target.Stop();

            _dataRepository.Received().SendAsync(
                Arg.Is<object>(c => ((Data)((DataVessel)c).data).SessionTimeLeft == "00:00:01"));
        }

        [Fact]
        public void Should_not_send_when_aggregator_not_dirty()
        {
            Data original = new Data()
            {
                SessionTimeLeft = "00:00:01"
            };

            _aggregator.AsData()
                .Returns(original);

            _aggregator.IsDirty.Returns(false);

            var target = new WebApiForwarderService(
                _aggregator,
                _dataRepository,
                _logger,
                1000,
                1);

            target.Start();

            Thread.Sleep(200);

            target.Stop();

            _dataRepository.Received(0).SendAsync(Arg.Any<DataVessel>());
        }
    }
}

using FuelAssistantMobile.DataGathering.SimhubPlugin;
using NSubstitute;
using PitWallAcquisitionPlugin.Aggregations;
using Xunit;

namespace PitWallAcquisitionPlugin.RunTime48.Tests
{
    public class MapSourceDataToAggregagtorTest
    {
        private ILiveAggregator _aggregator;
        private IPluginRecordRepository _record;
        private MappingConfigurationRepository _mapConfiguration;

        public MapSourceDataToAggregagtorTest()
        {
            // ARRANGE
            _aggregator = Substitute.For<ILiveAggregator>();
            _record = Substitute.For<IPluginRecordRepository>();

            _record.AirTemperature.Returns(1.0);
            _record.AvgRoadWetness.Returns(2.0);
            _record.LastLaptime.Returns("00:02:02.100");
            _record.SessionTimeLeft.Returns("03:03:03.100");

            _record.TyreWearFrontLeft.Returns(10.0);
            _record.TyreWearFrontRight.Returns(11.0);
            _record.TyreWearRearLeft.Returns(12.0);
            _record.TyreWearRearRight.Returns(13.0);

            _record.TyreFrontLeftTemperature.Returns(new FakeTyreTemperature() { Average = 21.0 });
            _record.TyreRearLeftTemperature.Returns(new FakeTyreTemperature() { Average = 22.0 });
            _record.TyreFrontRightTemperature.Returns(new FakeTyreTemperature() { Average = 23.0 });
            _record.TyreRearRightTemperature.Returns(new FakeTyreTemperature() { Average = 24.0 });

            _mapConfiguration = new MappingConfigurationRepository();

            // ACT
            MapSourceDataToAggregagtor.UpdateAggregatorNow(_aggregator, _record, _mapConfiguration);
        }

        [Fact]
        public void THEN_map_TyreFrontLeftTemperature()
        {
            _aggregator.Received(1).SetFrontLeftTyreTemperature(21.0);
        }

        [Fact]
        public void THEN_map_TyreRearLeftTemperature()
        {
            _aggregator.Received(1).SetRearLeftTyreTemperature(22.0);
        }

        [Fact]
        public void THEN_map_TyreFrontRightTemperature()
        {
            _aggregator.Received(1).SetFrontRightTyreTemperature(23.0);
        }

        [Fact]
        public void THEN_map_TyreRearRightTemperature()
        {
            _aggregator.Received(1).SetRearRightTyreTemperature(24.0);
        }


        // ----

        [Fact]
        public void THEN_map_TyreWearFrontLeft()
        {
            _aggregator.Received(1).SetFrontLeftTyreWear(10.0);
        }

        [Fact]
        public void THEN_map_TyreWearFrontRight()
        {
            _aggregator.Received(1).SetFrontRightTyreWear(11.0);
        }

        [Fact]
        public void THEN_map_TyreWearRearLeft()
        {
            _aggregator.Received(1).SetRearLeftTyreWear(12.0);
        }

        [Fact]
        public void THEN_map_TyreWearRearRight()
        {
            _aggregator.Received(1).SetRearRightTyreWear(13.0);
        }

        // ----

        [Fact]
        public void THEN_map_airTemperature()
        {
            _aggregator.Received(1).SetAirTemperature(1.0);
        }


        [Fact]
        public void THEN_map_AvgRoadWetness()
        {
            _aggregator.Received(1).SetAvgWetness(2.0);
        }

        [Fact]
        public void THEN_map_LapTime()
        {
            _aggregator.Received(1).SetLaptime("00:02:02.100");
        }

        [Fact]
        public void THEN_map_SessionTimeLeft()
        {
            _aggregator.Received(1).SetSessionTimeLeft("03:03:03.100");
        }
    }
}


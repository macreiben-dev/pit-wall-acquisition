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

        public MapSourceDataToAggregagtorTest()
        {
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
            _record.TyreRearRightTemperature.Returns(new FakeTyreTemperature() { Average = 23.0 });
        }

        [Fact]
        public void THEN_map_TyreWearFrontLeft()
        {
            MapSourceDataToAggregagtor.UpdateAggregatorNow(_aggregator, _record);

            _aggregator.Received(1).SetFrontLeftTyreWear(10.0);
        }

        [Fact]
        public void THEN_map_TyreWearFrontRight()
        {
            MapSourceDataToAggregagtor.UpdateAggregatorNow(_aggregator, _record);

            _aggregator.Received(1).SetFrontRightTyreWear(11.0);
        }

        [Fact]
        public void THEN_map_TyreWearRearLeft()
        {
            MapSourceDataToAggregagtor.UpdateAggregatorNow(_aggregator, _record);

            _aggregator.Received(1).SetRearLeftTyreWear(12.0);
        }

        [Fact]
        public void THEN_map_TyreWearRearRight()
        {
            MapSourceDataToAggregagtor.UpdateAggregatorNow(_aggregator, _record);

            _aggregator.Received(1).SetRearRightTyreWear(13.0);
        }

        // ----

        [Fact]
        public void THEN_map_airTemperature()
        {
            MapSourceDataToAggregagtor.UpdateAggregatorNow(_aggregator, _record);

            _aggregator.Received(1).SetAirTemperature(1.0);
        }


        [Fact]
        public void THEN_map_AvgRoadWetness()
        {
            MapSourceDataToAggregagtor.UpdateAggregatorNow(_aggregator, _record);

            _aggregator.Received(1).SetAvgWetness(2.0);
        }

        [Fact]
        public void THEN_map_LapTime()
        {
            MapSourceDataToAggregagtor.UpdateAggregatorNow(_aggregator, _record);

            _aggregator.Received(1).SetLaptime("00:02:02.100");
        }

        [Fact]
        public void THEN_map_SessionTimeLeft()
        {
            MapSourceDataToAggregagtor.UpdateAggregatorNow(_aggregator, _record);

            _aggregator.Received(1).SetSessionTimeLeft("03:03:03.100");
        }
    }
}


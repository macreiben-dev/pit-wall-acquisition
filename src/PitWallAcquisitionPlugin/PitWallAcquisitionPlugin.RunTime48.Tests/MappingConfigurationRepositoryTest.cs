using FuelAssistantMobile.DataGathering.SimhubPlugin;
using NSubstitute;
using PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators;
using Xunit;

namespace PitWallAcquisitionPlugin.RunTime48.Tests
{
    public class MappingConfigurationRepositoryTest
    {
        private ITelemetryLiveAggregator _aggregator;
        private IPluginRecordRepository _record;

        public MappingConfigurationRepositoryTest()
        {
            // ARRANGE
            _aggregator = Substitute.For<ITelemetryLiveAggregator>();
            _record = Substitute.For<IPluginRecordRepository>();

            _record.AirTemperature.Returns(1.0);
            _record.AvgRoadWetness.Returns(2.0);
            _record.TraceTemperature.Returns(3.0);

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

            _record.MaxFuel.Returns(30.0);
            _record.Fuel.Returns(31.0);
            _record.ComputedLastLapConsumption.Returns(32.0);
            _record.ComputedLiterPerLaps.Returns(33.0);
            _record.ComputedRemainingLaps.Returns(34.0);
            _record.ComputedRemainingTime.Returns("04:04:04.100");

            var target = new MappingConfigurationRepository();

            // ACT
            foreach (var item in target)
            {
                item.Set(_record, _aggregator);
            }
        }

        [Fact]
        public void THEN_map_MaxFuel()
        {
            _aggregator.Received(1).SetMaxFuel(30.0);
        }

        [Fact]
        public void THEN_map_Fuel()
        {
            _aggregator.Received(1).SetFuel(31.0);
        }

        [Fact]
        public void THEN_map_ComputedLastLapConsumptionl()
        {
            _aggregator.Received(1).SetComputedLastLapConsumption(32.0);
        }

        [Fact]
        public void THEN_map_ComputedLiterPerLaps()
        {
            _aggregator.Received(1).SetComputedLiterPerLaps(33.0);
        }

        [Fact]
        public void THEN_map_ComputedRemainingLaps()
        {
            _aggregator.Received(1).SetComputedRemainingLaps(34.0);
        }

        [Fact]
        public void THEN_map_ComputedRemainingTime()
        {
            _aggregator.Received(1).SetComputedRemainingTime("04:04:04.100");
        }

        // ----

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
        public void THEN_map_trackTemperature()
        {
            _aggregator.Received(1).SetTrackTemperature(3.0);
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


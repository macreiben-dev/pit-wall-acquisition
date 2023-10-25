using NFluent;
using NSubstitute;
using PitWallAcquisitionPlugin.PluginManagerWrappers;
using Xunit;

namespace PitWallAcquisitionPlugin.Tests.PluginManagerWrappers
{
    public class PluginManagerWrapperTest
    {
        private IPluginManagerAdapter _pluginManagerAdapter;

        public PluginManagerWrapperTest()
        {
            _pluginManagerAdapter = Substitute.For<IPluginManagerAdapter>();
        }

        private PluginManagerWrapper GetTarget()
        {
            return new PluginManagerWrapper(_pluginManagerAdapter);
        }

        [Fact]
        public void Should_map_is_gamingRunning()
        {
            _pluginManagerAdapter.GetPropertyValue("DataCorePlugin.GameRunning")
                .Returns(true);

            var target = GetTarget();

            Check.That(target.IsGameRunning).IsTrue();
        }

        [Fact]
        public void Should_map_SessionTimeLeft()
        {
            _pluginManagerAdapter.GetPropertyValue("DataCorePlugin.GameData.SessionTimeLeft")
                .Returns("01:02:03.00000");

            var target = GetTarget();

            Check.That(target.SessionTimeLeft).IsEqualTo("01:02:03.00000");
        }

        [Fact]
        public void Should_map_LastLapTime()
        {
            _pluginManagerAdapter.GetPropertyValue("DataCorePlugin.GameData.LastLapTime")
                .Returns("02:02:03.00000");

            var target = GetTarget();

            Check.That(target.LastLaptime).IsEqualTo("02:02:03.00000");
        }

        [Fact]
        public void Should_map_tyreWearFrontLeft()
        {
            _pluginManagerAdapter.GetPropertyValue("DataCorePlugin.GameData.TyreWearFrontLeft")
                .Returns(82.7055394649506);

            var target = GetTarget();

            Check.That(target.TyreWearFrontLeft).IsEqualTo(82.7055394649506);
        }

        [Fact]
        public void Should_map_tyreWearFrontRight()
        {
            _pluginManagerAdapter.GetPropertyValue("DataCorePlugin.GameData.TyreWearFrontRight")
                .Returns(82.7055884649506);

            var target = GetTarget();

            Check.That(target.TyreWearFrontRight).IsEqualTo(82.7055884649506);
        }

        [Fact]
        public void Should_map_tyreWearRearLeft()
        {
            _pluginManagerAdapter.GetPropertyValue("DataCorePlugin.GameData.TyreWearRearLeft")
                .Returns(82.7055884649544);

            var target = GetTarget();

            Check.That(target.TyreWearRearLeft).IsEqualTo(82.7055884649544);
        }

        [Fact]
        public void Should_map_tyreWearRearRight()
        {
            _pluginManagerAdapter.GetPropertyValue("DataCorePlugin.GameData.TyreWearRearRight")
                .Returns(82.9955884649544);

            var target = GetTarget();

            Check.That(target.TyreWearRearRight).IsEqualTo(82.9955884649544);
        }

        public class TyreFrontLeftTemperaturesTest
        {
            private const double Inner = 10.0;
            private const double Middle = 11.0;
            private const double Outer = 11.0;
            private const double Average = 11.0;

            private readonly IPluginManagerAdapter _pluginManagerAdapter;
            private double? _targetInner;
            private readonly double? _targetMiddle;
            private readonly double? _targetOuter;
            private readonly double? _targetAverage;

            public TyreFrontLeftTemperaturesTest()
            {
                _pluginManagerAdapter = Substitute.For<IPluginManagerAdapter>();

                _pluginManagerAdapter.GetPropertyValue("DataCorePlugin.GameData.TyreTemperatureFrontLeftInner").Returns(Inner);
                _pluginManagerAdapter.GetPropertyValue("DataCorePlugin.GameData.TyreTemperatureFrontLeftMiddle").Returns(Middle);
                _pluginManagerAdapter.GetPropertyValue("DataCorePlugin.GameData.TyreTemperatureFrontLeftOuter").Returns(Outer);
                _pluginManagerAdapter.GetPropertyValue("DataCorePlugin.GameData.TyreTemperatureFrontLeft").Returns(Average);

                var target = GetTarget();

                var tyreTemperature = target.TyreFrontLeftTemperature;

                _targetInner = tyreTemperature.Inner;
                _targetMiddle = tyreTemperature.Middle;
                _targetOuter = tyreTemperature.Outer;
                _targetAverage = tyreTemperature.Average;
            }

            private PluginManagerWrapper GetTarget()
            {
                return new PluginManagerWrapper(_pluginManagerAdapter);
            }

            [Fact]
            public void Should_map_inner_frontLeft_temperatures()
            {
                var target = GetTarget();

                Check.That(_targetInner).IsEqualTo(Inner);
            }

            [Fact]
            public void Should_map_inner_frontRight_temperatures()
            {
                var target = GetTarget();

                Check.That(_targetMiddle).IsEqualTo(Middle);
            }

            [Fact]
            public void Should_map_outer_frontRight_temperatures()
            {
                var target = GetTarget();

                Check.That(_targetOuter).IsEqualTo(Outer);
            }

            [Fact]
            public void Should_map_average_frontRight_temperatures()
            {
                var target = GetTarget();

                Check.That(_targetAverage).IsEqualTo(Average);
            }
        }

        public class TyreFrontRightTemperaturesTest
        {
            private const double Inner = 10.0;
            private const double Middle = 11.0;
            private const double Outer = 11.0;
            private const double Average = 11.0;

            private readonly IPluginManagerAdapter _pluginManagerAdapter;
            private readonly double? _targetInner;
            private readonly double? _targetMiddle;
            private readonly double? _targetOuter;
            private readonly double? _targetAverage;

            public TyreFrontRightTemperaturesTest()
            {
                _pluginManagerAdapter = Substitute.For<IPluginManagerAdapter>();

                _pluginManagerAdapter.GetPropertyValue("DataCorePlugin.GameData.TyreTemperatureFrontRightInner").Returns(Inner);
                _pluginManagerAdapter.GetPropertyValue("DataCorePlugin.GameData.TyreTemperatureFrontRightMiddle").Returns(Middle);
                _pluginManagerAdapter.GetPropertyValue("DataCorePlugin.GameData.TyreTemperatureFrontRightOuter").Returns(Outer);
                _pluginManagerAdapter.GetPropertyValue("DataCorePlugin.GameData.TyreTemperatureFrontRight").Returns(Average);

                var target = GetTarget();

                var tyreTemperature = target.TyreFrontRightTemperature;

                _targetInner = tyreTemperature.Inner;
                _targetMiddle = tyreTemperature.Middle;
                _targetOuter = tyreTemperature.Outer;
                _targetAverage = tyreTemperature.Average;
            }

            private PluginManagerWrapper GetTarget()
            {
                return new PluginManagerWrapper(_pluginManagerAdapter);
            }

            [Fact]
            public void Should_map_inner_frontLeft_temperatures()
            {
                var target = GetTarget();

                Check.That(_targetInner).IsEqualTo(Inner);
            }

            [Fact]
            public void Should_map_inner_frontRight_temperatures()
            {
                var target = GetTarget();

                Check.That(_targetMiddle).IsEqualTo(Middle);
            }

            [Fact]
            public void Should_map_outer_frontRight_temperatures()
            {
                var target = GetTarget();

                Check.That(_targetOuter).IsEqualTo(Outer);
            }

            [Fact]
            public void Should_map_average_frontRight_temperatures()
            {
                var target = GetTarget();

                Check.That(_targetAverage).IsEqualTo(Average);
            }
        }

        public class TyreRearLeftTemperaturesTest
        {
            private const double Inner = 10.0;
            private const double Middle = 11.0;
            private const double Outer = 11.0;
            private const double Average = 11.0;

            private readonly IPluginManagerAdapter _pluginManagerAdapter;
            private readonly double? _targetInner;
            private readonly double? _targetMiddle;
            private readonly double? _targetOuter;
            private readonly double? _targetAverage;

            public TyreRearLeftTemperaturesTest()
            {
                _pluginManagerAdapter = Substitute.For<IPluginManagerAdapter>();

                _pluginManagerAdapter.GetPropertyValue("DataCorePlugin.GameData.TyreTemperatureRearLeftInner").Returns(Inner);
                _pluginManagerAdapter.GetPropertyValue("DataCorePlugin.GameData.TyreTemperatureRearLeftMiddle").Returns(Middle);
                _pluginManagerAdapter.GetPropertyValue("DataCorePlugin.GameData.TyreTemperatureRearLeftOuter").Returns(Outer);
                _pluginManagerAdapter.GetPropertyValue("DataCorePlugin.GameData.TyreTemperatureRearLeft").Returns(Average);

                var target = GetTarget();

                var tyreTemperature = target.TyreRearLeftTemperature;

                _targetInner = tyreTemperature.Inner;
                _targetMiddle = tyreTemperature.Middle;
                _targetOuter = tyreTemperature.Outer;
                _targetAverage = tyreTemperature.Average;
            }

            private PluginManagerWrapper GetTarget()
            {
                return new PluginManagerWrapper(_pluginManagerAdapter);
            }

            [Fact]
            public void Should_map_inner_frontLeft_temperatures()
            {
                var target = GetTarget();

                Check.That(_targetInner).IsEqualTo(Inner);
            }

            [Fact]
            public void Should_map_inner_frontRight_temperatures()
            {
                var target = GetTarget();

                Check.That(_targetMiddle).IsEqualTo(Middle);
            }

            [Fact]
            public void Should_map_outer_frontRight_temperatures()
            {
                var target = GetTarget();

                Check.That(_targetOuter).IsEqualTo(Outer);
            }

            [Fact]
            public void Should_map_average_frontRight_temperatures()
            {
                var target = GetTarget();

                Check.That(_targetAverage).IsEqualTo(Average);
            }
        }

        public class TyreRearRightTemperaturesTest
        {
            private const double Inner = 10.0;
            private const double Middle = 11.0;
            private const double Outer = 11.0;
            private const double Average = 11.0;

            private readonly IPluginManagerAdapter _pluginManagerAdapter;
            private readonly double? _targetInner;
            private readonly double? _targetMiddle;
            private readonly double? _targetOuter;
            private readonly double? _targetAverage;

            public TyreRearRightTemperaturesTest()
            {
                _pluginManagerAdapter = Substitute.For<IPluginManagerAdapter>();

                _pluginManagerAdapter.GetPropertyValue("DataCorePlugin.GameData.TyreTemperatureRearRightInner").Returns(Inner);
                _pluginManagerAdapter.GetPropertyValue("DataCorePlugin.GameData.TyreTemperatureRearRightMiddle").Returns(Middle);
                _pluginManagerAdapter.GetPropertyValue("DataCorePlugin.GameData.TyreTemperatureRearRightOuter").Returns(Outer);
                _pluginManagerAdapter.GetPropertyValue("DataCorePlugin.GameData.TyreTemperatureRearRight").Returns(Average);

                var target = GetTarget();

                var tyreTemperature = target.TyreRearRightTemperature;

                _targetInner = tyreTemperature.Inner;
                _targetMiddle = tyreTemperature.Middle;
                _targetOuter = tyreTemperature.Outer;
                _targetAverage = tyreTemperature.Average;
            }

            private PluginManagerWrapper GetTarget()
            {
                return new PluginManagerWrapper(_pluginManagerAdapter);
            }

            [Fact]
            public void Should_map_inner_frontLeft_temperatures()
            {
                Check.That(_targetInner).IsEqualTo(Inner);
            }

            [Fact]
            public void Should_map_inner_frontRight_temperatures()
            {
                Check.That(_targetMiddle).IsEqualTo(Middle);
            }

            [Fact]
            public void Should_map_outer_frontRight_temperatures()
            {
                Check.That(_targetOuter).IsEqualTo(Outer);
            }

            [Fact]
            public void Should_map_average_frontRight_temperatures()
            {
                Check.That(_targetAverage).IsEqualTo(Average);
            }
        }

        public class WeatherCondition
        {
            private IPluginManagerAdapter _pluginManagerAdapter;
            
            private double? _roadWetness;
            private double? _raining;
            private double? _airTemperature;

            private const double RoadWetness = 10.0;
            private const double Raining = 11.0;
            private const double AirTemperature = 12.0;

            public WeatherCondition()
            {
                _pluginManagerAdapter = Substitute.For<IPluginManagerAdapter>();

                _pluginManagerAdapter.GetPropertyValue("DataCorePlugin.GameRawData.Scoring.mScoringInfo.mAvgPathWetness")
                    .Returns(RoadWetness);

                _pluginManagerAdapter.GetPropertyValue("GameRawData.Scoring.mScoringInfo.mRaining")
                    .Returns(Raining);

                _pluginManagerAdapter.GetPropertyValue("DataCorePlugin.GameData.AirTemperature")
                    .Returns(AirTemperature);

                var target = GetTarget();

                _roadWetness = target.AvgRoadWetness;
                _raining = target.Raining;
                _airTemperature = target.AirTemperature;
            }

            private PluginManagerWrapper GetTarget()
            {
                return new PluginManagerWrapper(_pluginManagerAdapter);
            }

            [Fact]
            public void THEN_map_averageWetNess()
            {
                Check.That(_roadWetness).IsEqualTo(RoadWetness);
            }

            [Fact]
            public void THEN_map_raining()
            {
                Check.That(_raining).IsEqualTo(Raining);
            }

            [Fact]
            public void THEN_map_airTemperature()
            {
                Check.That(_airTemperature).IsEqualTo(AirTemperature);
            }
        }
    }
}

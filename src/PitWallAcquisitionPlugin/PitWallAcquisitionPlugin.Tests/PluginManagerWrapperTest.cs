using FuelAssistantMobile.DataGathering.SimhubPlugin.PluginManagerWrappers;
using NFluent;
using NSubstitute;
using PitWallAcquisitionPlugin.PluginManagerWrappers;
using Xunit;

namespace PitWallAcquisitionPlugin.Tests
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
    }
}

using NFluent;
using NSubstitute;
using PitWallAcquisitionPlugin.PluginManagerWrappers;
using Xunit;

namespace PitWallAcquisitionPlugin.RunTime48.Tests.PluginManagerWrappers
{
    public class LeaderboardEntryTest
    {
        private readonly IPluginManagerAdapter _pluginAdapter;

        public LeaderboardEntryTest()
        {
            _pluginAdapter = Substitute.For<IPluginManagerAdapter>();
        }

        private LeaderboardEntry GetTarget(int position)
        {
            return new LeaderboardEntry(_pluginAdapter, position);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(15)]
        [InlineData(80)]
        public void GIVEN_pluginAdapter_AND_position_THEN_return_lastLapInScondsint(int position)
        {
            // ARRANGE
            string metricName = string.Format(
                "GarySwallowDataPlugin.Leaderboard.Position{0:00}.LastLap", 
                position);

        _pluginAdapter.GetPropertyValue(metricName)
                .Returns(122.2);

            // ACT
            var target = GetTarget(position);

            double actual = target.LastLapInSeconds;

            // ASSERT
            Check.That(actual).IsEqualTo(122.2);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(15)]
        [InlineData(80)]
        public void GIVEN_pluginAdapter_AND_position_THEN_return_CarName(int position)
        {
            // ARRANGE
            string metricName = string.Format(
                "GarySwallowDataPlugin.Leaderboard.Position{0:00}.CarName", 
                position);

            _pluginAdapter.GetPropertyValue(metricName)
                .Returns("SomeCarName01");

            // ACT
            var target = GetTarget(position);

            string actual = target.CarName;

            // ASSERT
            Check.That(actual).IsEqualTo("SomeCarName01");
        }


        [Theory]
        [InlineData(1)]
        [InlineData(15)]
        [InlineData(80)]
        public void GIVEN_pluginAdapter_AND_position_THEN_return_position(int position)
        {
            // ARRANGE
            string metricName = string.Format(
                "GarySwallowDataPlugin.Leaderboard.Position{0:00}.LastLap",
                position);

            _pluginAdapter.GetPropertyValue(metricName)
                    .Returns(122.2);

            // ACT
            var target = GetTarget(position);

            int actual = target.Position;

            // ASSERT
            Check.That(actual).IsEqualTo(position);
        }
    }
}

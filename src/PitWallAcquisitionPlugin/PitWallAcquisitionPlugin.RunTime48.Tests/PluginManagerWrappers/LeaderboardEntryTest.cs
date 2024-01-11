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

        [Fact]
        public void GIVEN_pluginAdapter_AND_position_THEN_return_lastLapInSconds()
        {
            // ARRANGE
            _pluginAdapter.GetPropertyValue("GarySwallowDataPlugin.Leaderboard.Position01.LastLap")
                .Returns(122.2);

            // ACT
            var target = GetTarget(1);

            double actual = target.LasLapInSeconds;

            // ASSERT
            Check.That(actual).IsEqualTo(122.2);
        }

        [Fact]
        public void GIVEN_pluginAdapter_AND_position_THEN_return_CarName()
        {
            // ARRANGE
            _pluginAdapter.GetPropertyValue("GarySwallowDataPlugin.Leaderboard.Position01.CarName")
                .Returns("SomeCarName01");

            // ACT
            var target = GetTarget(1);

            string actual = target.CarName;

            // ASSERT
            Check.That(actual).IsEqualTo("SomeCarName01");
        }
    }
}

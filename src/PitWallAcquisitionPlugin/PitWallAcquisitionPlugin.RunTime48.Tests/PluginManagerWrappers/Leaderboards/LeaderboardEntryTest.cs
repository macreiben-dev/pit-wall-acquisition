using NFluent;
using NSubstitute;
using PitWallAcquisitionPlugin.PluginManagerWrappers;
using PitWallAcquisitionPlugin.PluginManagerWrappers.Leaderboards;
using Xunit;

namespace PitWallAcquisitionPlugin.RunTime48.Tests.PluginManagerWrappers.Leaderboards
{
    public class LeaderboardEntryTest
    {
        private const string GaryswallowdatapluginLeaderboardPositionInpitlane = "GarySwallowDataPlugin.Leaderboard.Position{0:00}.InPitLane";
        private const string GaryswallowdatapluginLeaderboardPositionInpitbox = "GarySwallowDataPlugin.Leaderboard.Position{0:00}.InPitBox";

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

        [Theory]
        [InlineData(1)]
        [InlineData(15)]
        [InlineData(80)]
        public void GIVEN_pluginAdapter_AND_position_WHEN_inPitlane_THEN_return_true(int position)
        {
            // ARRANGE
            string metricName = string.Format(
                GaryswallowdatapluginLeaderboardPositionInpitlane,
                position);

            _pluginAdapter.GetPropertyValue(metricName)
                .Returns(1);

            // ACT
            var target = GetTarget(position);

            bool actual = target.InPitLane;

            // ASSERT
            Check.That(actual).IsEqualTo(true);
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(15)]
        [InlineData(80)]
        public void GIVEN_pluginAdapter_AND_position_WHEN_not_inPitlane_THEN_return_false(int position)
        {
            // ARRANGE
            string metricName = string.Format(
                GaryswallowdatapluginLeaderboardPositionInpitlane,
                position);

            _pluginAdapter.GetPropertyValue(metricName)
                .Returns(0);

            // ACT
            var target = GetTarget(position);

            bool actual = target.InPitLane;

            // ASSERT
            Check.That(actual).IsEqualTo(false);
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(15)]
        [InlineData(80)]
        public void GIVEN_pluginAdapter_AND_position_WHEN_inPitlane_isNull_THEN_return_false(int position)
        {
            // ARRANGE
            string metricName = string.Format(
                GaryswallowdatapluginLeaderboardPositionInpitlane,
                position);

            _pluginAdapter.GetPropertyValue(metricName)
                .Returns(null);

            // ACT
            var target = GetTarget(position);

            bool actual = target.InPitLane;

            // ASSERT
            Check.That(actual).IsEqualTo(false);
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(15)]
        [InlineData(80)]
        public void GIVEN_pluginAdapter_AND_position_WHEN_inPitBox_THEN_return_true(int position)
        {
            // ARRANGE
            string metricName = string.Format(
                GaryswallowdatapluginLeaderboardPositionInpitbox,
                position);

            _pluginAdapter.GetPropertyValue(metricName)
                .Returns(1);

            // ACT
            var target = GetTarget(position);

            bool actual = target.InPitBox;

            // ASSERT
            Check.That(actual).IsEqualTo(true);
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(15)]
        [InlineData(80)]
        public void GIVEN_pluginAdapter_AND_position_WHEN_not_inPitBox_THEN_return_true(int position)
        {
            // ARRANGE
            string metricName = string.Format(
                GaryswallowdatapluginLeaderboardPositionInpitbox,
                position);

            _pluginAdapter.GetPropertyValue(metricName)
                .Returns(0);

            // ACT
            var target = GetTarget(position);

            bool actual = target.InPitBox;

            // ASSERT
            Check.That(actual).IsEqualTo(false);
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(15)]
        [InlineData(80)]
        public void GIVEN_pluginAdapter_AND_position_WHEN_inPitBox_isNull_THEN_return_false(int position)
        {
            // ARRANGE
            string metricName = string.Format(
                GaryswallowdatapluginLeaderboardPositionInpitbox,
                position);

            _pluginAdapter.GetPropertyValue(metricName)
                .Returns(null);

            // ACT
            var target = GetTarget(position);

            bool actual = target.InPitBox;

            // ASSERT
            Check.That(actual).IsEqualTo(false);
        }
        
    }
}
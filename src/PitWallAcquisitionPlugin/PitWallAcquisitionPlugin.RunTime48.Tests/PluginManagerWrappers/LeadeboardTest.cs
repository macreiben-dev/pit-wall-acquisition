using FuelAssistantMobile.DataGathering.SimhubPlugin;
using NFluent;
using NSubstitute;
using PitWallAcquisitionPlugin.PluginManagerWrappers;
using System.Linq;
using System.Xml;
using Xunit;

namespace PitWallAcquisitionPlugin.RunTime48.Tests.PluginManagerWrappers
{
    public class LeadeboardCollectionTest
    {
        [Fact]
        public void WHEN_leaderboard_is_generated_THEN_retun_99_elements()
        {
            var adapter = Substitute.For<IPluginManagerAdapter>();

            adapter.GetPropertyValue("GarySwallowDataPlugin.Leaderboard.Position01.LastLap")
                .Returns(122.2);


            adapter.GetPropertyValue("GarySwallowDataPlugin.Leaderboard.Position15.LastLap")
                .Returns(122.2);


            var target = new LeadeboardCollection(adapter);

            var actual = target.ToList();

            Check.That(actual).HasSize(99);

        }

        [Fact]
        public void WHEN_invoking_twice_THEN_return_same_instance()
        {
            var adapter = Substitute.For<IPluginManagerAdapter>();

            adapter.GetPropertyValue("GarySwallowDataPlugin.Leaderboard.Position01.LastLap")
                .Returns(122.2);

            var target = new LeadeboardCollection(adapter);

            var firstCount = target.ToList().Count(c => c.LastLapInSeconds == 122.2);

            adapter.GetPropertyValue("GarySwallowDataPlugin.Leaderboard.Position15.LastLap")
                .Returns(122.2);

            var secondCount = target.ToList().Count(c => c.LastLapInSeconds == 122.2);

            Check.That(firstCount).IsEqualTo(1);
            Check.That(secondCount).IsEqualTo(1);
        }
    }
}

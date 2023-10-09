using PitWallAcquisitionPlugin.UI.ViewModels;
using NFluent;
using FluentAssertions;
using System.ComponentModel;

namespace PitWallAcquisitionPlugin.Tests.UI.ViewModels
{
    public class PluginSettingsViewModelTest
    {
        [Fact]
        public void GIVEN_pilotName_isSet_THEN_notifyPropertychanged_pilotName() {

            var target = new PluginSettingsViewModel();

            using var monitored = target.Monitor();

            target.PilotName = "some_name";

            monitored.Should().Raise("PropertyChanged")
                .WithSender(target)
                .WithArgs<PropertyChangedEventArgs>(
                    e => e.PropertyName == "PilotName");
        }

        [Fact]
        public void GIVEN_pilotName_isSet_THEN_pilotName_hasExpectedValue()
        {
            var target = new PluginSettingsViewModel();

            target.PilotName = "some_name";

            Check.That(target.PilotName).IsEqualTo("some_name");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("      ")]
        public void GIVEN_pilotName_isNotSet_THEN_dataErrorInfo_returns_notSet(string input)
        {
            var target = new PluginSettingsViewModel();

            target.PilotName = input;

            Check.That(target["PilotName"]).IsEqualTo("Pilot name must be set.");
        }
    }
}

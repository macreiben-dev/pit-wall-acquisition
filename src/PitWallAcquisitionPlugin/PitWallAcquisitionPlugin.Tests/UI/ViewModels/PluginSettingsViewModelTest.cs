using PitWallAcquisitionPlugin.UI.ViewModels;
using NFluent;
using FluentAssertions;
using System.ComponentModel;

namespace PitWallAcquisitionPlugin.Tests.UI.ViewModels
{
    public class PluginSettingsViewModelTest
    {
        private const string VALID_API_ADDRESS = "http://api.address.net";

        [Fact]
        public void GIVEN_pilotName_isSet_THEN_notifyPropertychanged_pilotName()
        {

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

        [Fact]
        public void GIVEN_ApiAddress_isSet_THEN_notifyPropertychanged_ApiAdress()
        {
            var target = new PluginSettingsViewModel();

            using var monitored = target.Monitor();

            target.ApiAddress = VALID_API_ADDRESS;

            monitored.Should().Raise("PropertyChanged")
                .WithSender(target)
                .WithArgs<PropertyChangedEventArgs>(
                    e => e.PropertyName == "ApiAddress");
        }

        [Fact]
        public void GIVEN_ApiAddress_isSet_THEN_ApiAddress_hasExpectedValue()
        {
            var target = new PluginSettingsViewModel();

            target.ApiAddress = VALID_API_ADDRESS;

            Check.That(target.ApiAddress).IsEqualTo(VALID_API_ADDRESS);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("      ")]
        public void GIVEN_ApiAddress_isNotSet_THEN_dataErrorInfo_returns_notSet(string input)
        {
            var target = new PluginSettingsViewModel();

            target.ApiAddress = input;

            Check.That(target["ApiAddress"]).IsEqualTo("API address must be set.");
        }

        [Fact]
        public void GIVEN_apiAddress_uri_isValid_THEN_error_isNull()
        {
            var target = new PluginSettingsViewModel();

            target.ApiAddress = VALID_API_ADDRESS;

            var actual = target["ApiAddress"];

            Check.That(actual).IsNull();
        }

        [Theory]
        [InlineData("htttttp://....ext")]
        [InlineData("http://test,test2.ext")]
        [InlineData("http://test=test2.ext")]
        public void GIVEN_apiAddress_uri_isNotValid_THEN_error_isSet(string input)
        {
            var target = new PluginSettingsViewModel();

            target.ApiAddress = input;

            var actual = target["ApiAddress"];

            Check.That(actual).IsEqualTo(
                "API URI format is invalid. Should look like http://domain.ext or http://domain.ext");
        }
    }
}

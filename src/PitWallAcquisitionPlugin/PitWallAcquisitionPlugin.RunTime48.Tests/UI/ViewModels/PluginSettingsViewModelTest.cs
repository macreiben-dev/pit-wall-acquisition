using PitWallAcquisitionPlugin.UI.ViewModels;
using NFluent;
using FluentAssertions;
using System.ComponentModel;
using Xunit;
using NSubstitute;
using PitWallAcquisitionPlugin.Aggregations;

namespace PitWallAcquisitionPlugin.Tests.UI.ViewModels
{
    public class PluginSettingsViewModelTest
    {
        private const string VALID_API_ADDRESS = "http://api.address.net";
        private FakePitWallConfiguration _pitWallConfiguration;
        private ILiveAggregator _aggregator;

        public PluginSettingsViewModelTest()
        {

            _pitWallConfiguration = new FakePitWallConfiguration();

            _aggregator = new LiveAggregator(); // oustide in here.
        }

        private PluginSettingsViewModel GetTarget()
        {
            return new PluginSettingsViewModel(_pitWallConfiguration, _aggregator);
        }

        [Fact]
        public void GIVEN_pilotName_isSet_THEN_notifyPropertychanged_pilotName()
        {
            PluginSettingsViewModel target = GetTarget();

            using (var monitored = target.Monitor())
            {
                target.PilotName = "some_name";

                monitored.Should().Raise("PropertyChanged")
                    .WithSender(target)
                    .WithArgs<PropertyChangedEventArgs>(
                        e => e.PropertyName == "PilotName");
            }
        }

        [Fact]
        public void GIVEN_pilotName_isSet_THEN_pilotName_hasExpectedValue()
        {
            PluginSettingsViewModel target = GetTarget();

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
            PluginSettingsViewModel target = GetTarget();

            target.PilotName = input;

            Check.That(target["PilotName"]).IsEqualTo("Pilot name must be set.");
        }

        [Fact]
        public void GIVEN_pilotName_isSet_AND_noError_THEN_updateAggregator()
        {
            string personalKey = "some_valid_key_ok";

            // ACT
            var target = GetTarget();

            target.PilotName = personalKey;

            var actual = _aggregator.AsData();

            // ASSERT
            Check.That(actual.PilotName).IsEqualTo(personalKey);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("      ")]
        public void GIVEN_pilotName_isSet_AND_error_THEN_doNotUpdateUpdate_aggregator(string input)
        {
            // ACT
            var target = GetTarget();

            target.PilotName = input;

            var actual = _aggregator.AsData();

            // ASSERT
            Check.That(actual.PilotName).IsNull();
        }

        #region Api Address

        [Fact]
        public void GIVEN_ApiAddress_isSet_THEN_notifyPropertychanged_ApiAdress()
        {
            PluginSettingsViewModel target = GetTarget();

            using (var monitored = target.Monitor())
            {
                target.ApiAddress = VALID_API_ADDRESS;

                monitored.Should().Raise("PropertyChanged")
                    .WithSender(target)
                    .WithArgs<PropertyChangedEventArgs>(
                        e => e.PropertyName == "ApiAddress");
            }
        }

        [Fact]
        public void GIVEN_ApiAddress_isSet_THEN_ApiAddress_hasExpectedValue()
        {
            PluginSettingsViewModel target = GetTarget();

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
            var target = GetTarget();

            target.ApiAddress = input;

            Check.That(target["ApiAddress"]).IsEqualTo("API address must be set.");
        }

        [Fact]
        public void GIVEN_apiAddress_uri_isValid_THEN_error_isNull()
        {
            var target = GetTarget();

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
            var target = GetTarget();

            target.ApiAddress = input;

            var actual = target["ApiAddress"];

            Check.That(actual).IsEqualTo(
                "API URI format is invalid. Should look like http://domain.ext or http://domain.ext");
        }

        #endregion Api Address

        #region Personal key

        [Fact]
        public void GIVEN_personalKey_isSet_THEN_personalKey_hasExpectedValue()
        {
            var target = GetTarget();

            target.PersonalKey = "some_key";

            Check.That(target.PersonalKey).IsEqualTo("some_key");
        }

        [Theory]
        [InlineData("1")]
        [InlineData("12")]
        [InlineData("123")]
        [InlineData("1234")]
        [InlineData("12345")]
        [InlineData("123456")]
        [InlineData("1234567")]
        [InlineData("12345678")]
        [InlineData("123456789")]
        public void GIVEN_personalKey_isLessThan_10character_THEN_fail(string input)
        {
            var target = GetTarget();

            target.PersonalKey = input;

            Check.That(target["PersonalKey"]).IsEqualTo("Personal key length should be at least 10 character long.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("     ")]
        public void GIVEN_personalKey_isNullOrEmptyOrWhiteSpace_THEN_fail(string input)
        {
            var target = GetTarget();

            target.PersonalKey = input;

            Check.That(target["PersonalKey"])
                .IsEqualTo("Personal should be made of alphanumerical character and \"-\", \"_\", \"@\".");
        }

        [Fact]
        public void GIVEN_personalKey_isSet_THEN_notifyPropertychanged_personalKey()
        {
            var target = GetTarget();

            using (var monitored = target.Monitor())
            {

                target.PersonalKey = "some_name";

                monitored.Should().Raise("PropertyChanged")
                    .WithSender(target)
                    .WithArgs<PropertyChangedEventArgs>(
                        e => e.PropertyName == "PersonalKey");
            }
        }

        [Fact]
        public void GIVEN_personalKey_isSet_AND_noError_THEN_updateAggregator()
        {
            string personalKey = "some_valid_key_ok";

            // ACT
            var target = GetTarget();

            target.PersonalKey = personalKey;

            var actual = _aggregator.AsData();

            // ASSERT
            Check.That(actual.SimerKey).IsEqualTo(personalKey);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("     ")]
        public void GIVEN_personalKey_isSet_AND_error_THEN_doNotUpdateUpdate_aggregator(string input)
        {
            string personalKey = input;

            // ACT
            var target = GetTarget();

            target.PersonalKey = personalKey;

            var actual = _aggregator.AsData();

            // ASSERT
            Check.That(actual.SimerKey).IsNull();
        }

        #endregion Personal key
    }
}

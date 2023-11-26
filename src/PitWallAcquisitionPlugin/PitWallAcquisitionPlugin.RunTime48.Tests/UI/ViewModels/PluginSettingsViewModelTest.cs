﻿using PitWallAcquisitionPlugin.UI.ViewModels;
using NFluent;
using FluentAssertions;
using System.ComponentModel;
using Xunit;
using NSubstitute;
using PitWallAcquisitionPlugin.HealthChecks;
using PitWallAcquisitionPlugin.Aggregations.Aggregators;

namespace PitWallAcquisitionPlugin.Tests.UI.ViewModels
{
    public class PluginSettingsViewModelTest
    {
        private const string VALID_API_ADDRESS = "http://api.address.net";
        private FakePitWallConfiguration _pitWallConfiguration;
        private IPluginSettingsCommandFactory _isApiAvailableCommand;

        public PluginSettingsViewModelTest()
        {
            _pitWallConfiguration = new FakePitWallConfiguration()
            {
                PilotName = "SomePilot1"
            };

            _isApiAvailableCommand = Substitute.For<IPluginSettingsCommandFactory>();
        }

        private PluginSettingsViewModel GetTarget()
        {
            return new PluginSettingsViewModel(
                _pitWallConfiguration,
                _isApiAvailableCommand);
        }

        public PluginSettingsViewModel GetTargetWithPersonalKey(string personalKey)
        {
            return new PluginSettingsViewModel(
                _pitWallConfiguration,
                _isApiAvailableCommand);
        }

        private PluginSettingsViewModel GetTargetWithRealCommand()
        {
            return new PluginSettingsViewModel(
                _pitWallConfiguration,
                new PluginSettingsCommandFactory(
                    Substitute.For<IHealthCheckService>(),
                    Substitute.For<IPitWallConfiguration>()));
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

        #region CarName 

        [Fact]
        public void GIVEN_CarName_isSet_THEN_notifyPropertychanged_isRaised()
        {
            PluginSettingsViewModel target = GetTarget();

            using (var monitored = target.Monitor())
            {
                target.CarName = "some_name";

                monitored.Should().Raise("PropertyChanged")
                    .WithSender(target)
                    .WithArgs<PropertyChangedEventArgs>(
                        e => e.PropertyName == "CarName");
            }
        }


        [Fact]
        public void GIVEN_CarName_isSet_THEN_CarName_hasExpectedValue()
        {
            PluginSettingsViewModel target = GetTarget();

            target.CarName = "some_name";

            Check.That(target.CarName).IsEqualTo("some_name");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("      ")]
        public void GIVEN_CarName_isNotSet_THEN_dataErrorInfo_returns_notSet(string input)
        {
            PluginSettingsViewModel target = GetTarget();

            target.CarName = input;

            Check.That(target["CarName"]).IsEqualTo("Car name must be set.");
        }

        #endregion CarName 

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
        public void GIVEN_ApiAddress_isSet_THEN_raise_isApiAvailableCanExecutechanged()
        {
            PluginSettingsViewModel target = GetTargetWithRealCommand();

            using (var monitored = target.IsApiAvailableCommand.Monitor())
            {
                target.ApiAddress = VALID_API_ADDRESS;

                monitored.Should().Raise("CanExecuteChanged");
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
        public void GIVEN_personalKey_isSet_THEN_raise_isApiAvailableCanExecutechanged()
        {
            PluginSettingsViewModel target = GetTargetWithRealCommand();

            using (var monitored = target.IsApiAvailableCommand.Monitor())
            {
                target.PersonalKey = "some_name";

                monitored.Should().Raise("CanExecuteChanged");
            }
        }


        #endregion Personal key

        #region IsApiAvailable 

        [Fact]
        public void GIVEN_class_is_instanciated_THEN_isApiAvailable_isNotNull()
        {
            var target = GetTargetWithRealCommand();

            Check.That(target.IsApiAvailableCommand).IsNotNull();
        }

        [Theory]
        [InlineData("data1")]
        [InlineData("data2")]
        [InlineData("data3")]
        public void GIVEN_isApiAvailable_IsSet_THEN_property_has_exepectedValue(string input)
        {
            var target = GetTarget();

            target.IsApiAvailable = input;

            Check.That(target.IsApiAvailable).IsEqualTo(input);
        }

        [Fact]
        public void GIVEN_isApiAvailable_isSet_THEN_notifyPropertychanged_isApiAvailable()
        {
            var target = GetTarget();

            using (var monitored = target.Monitor())
            {

                target.IsApiAvailable = "some_name";

                monitored.Should().Raise("PropertyChanged")
                    .WithSender(target)
                    .WithArgs<PropertyChangedEventArgs>(
                        e => e.PropertyName == "IsApiAvailable");
            }
        }

        #endregion IsApiAvailable 

        #region SaveConfig

        [Fact]
        public void GIVEN_class_is_instanciated_THEN_saveConfigurationCommandIsSet()
        {
            var target = GetTargetWithRealCommand();

            Check.That(target.SaveToConfigurationCommand).IsNotNull();
        }

        #endregion SaveConfig

        #region Constructor

        [Fact]
        public void GIVEN_configuration_saved_THEN_load_pilotName()
        {
            _pitWallConfiguration.PilotName = "SomePilotName";

            var target = GetTarget();

            Check.That(target.PilotName).IsEqualTo("SomePilotName");
        }

        [Fact]
        public void GIVEN_configuration_saved_THEN_load_carName()
        {
            _pitWallConfiguration.CarName = "SomeCarName";

            var target = GetTarget();

            Check.That(target.CarName).IsEqualTo("SomeCarName");
        }


        [Fact]
        public void GIVEN_configuration_saved_THEN_load_apiAddress()
        {
            _pitWallConfiguration.ApiAddress = "http://api.data.net";

            var target = GetTarget();

            Check.That(target.ApiAddress).IsEqualTo("http://api.data.net");
        }


        [Fact]
        public void GIVEN_configuration_saved_THEN_load_personalKey()
        {
            _pitWallConfiguration.PersonalKey = "some_test_looking_value_2023";

            var target = GetTarget();

            Check.That(target.PersonalKey).IsEqualTo("some_test_looking_value_2023");
        }
        #endregion Constructor
    }
}

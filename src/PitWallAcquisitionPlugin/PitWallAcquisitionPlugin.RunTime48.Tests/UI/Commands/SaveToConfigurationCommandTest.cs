﻿using NFluent;
using NSubstitute;
using PitWallAcquisitionPlugin.Tests.UI.ViewModels;
using PitWallAcquisitionPlugin.UI.Commands;
using PitWallAcquisitionPlugin.UI.ViewModels;
using Xunit;

namespace PitWallAcquisitionPlugin.RunTime48.Tests.UI.Commands
{
    public class SaveToConfigurationCommandTest
    {
        private FakePitWallConfiguration _configuration;
        private ISettingsValidator _settingsValidator;

        public SaveToConfigurationCommandTest()
        {
            _configuration = new FakePitWallConfiguration();

            _settingsValidator = Substitute.For<ISettingsValidator>();
        }

        private ISaveToConfigurationCommand GetTarget()
        {
            return new SaveToConfigurationCommand(_configuration, _settingsValidator);
        }

        [Fact]
        public void GIVEN_parameter_is_null_THEN_canExecute_return_false()
        {
            var target = GetTarget();

            var actual = target.CanExecute(null);

            Check.That(actual).IsFalse();
        }

        [Fact]
        public void GIVEN_configurationIsValid_AND_execute_invoked_THEN_pitwallConfiguration_pilotName_is_updated()
        {
            var original = new FakeUserDefinedConfiguration()
            {
                PilotName = "SomePilotName"
            };

            SetAllValidationOk();

            var target = GetTarget();

            target.Execute(original);

            Check.That(_configuration.PilotName).IsEqualTo("SomePilotName");
        }

        [Fact]
        public void GIVEN_configurationIsNull_THEN_pilotName_is_notSet()
        {
            var target = GetTarget();

            target.Execute(null);

            Check.That(_configuration.PilotName).IsNull();
        }

        [Fact]
        public void GIVEN_configurationIsValid_AND_execute_invoked_THEN_pitwallConfiguration_personalKey_is_updated()
        {
            var original = new FakeUserDefinedConfiguration()
            {
                PersonalKey = "SomeData"
            };

            SetAllValidationOk();

            var target = GetTarget();

            target.Execute(original);

            Check.That(_configuration.PersonalKey).IsEqualTo("SomeData");
        }

        [Fact]
        public void GIVEN_configurationIsNull_THEN_personalKey_is_notSet()
        {
            var target = GetTarget();

            target.Execute(null);

            Check.That(_configuration.PersonalKey).IsNull();
        }

        [Fact]
        public void GIVEN_configurationIsValid_AND_execute_invoked_THEN_pitwallConfiguration_apiAddress_is_updated()
        {
            var original = new FakeUserDefinedConfiguration()
            {
                ApiAddress = "SomeData"
            };

            SetAllValidationOk();

            var target = GetTarget();

            target.Execute(original);

            Check.That(_configuration.ApiAddress).IsEqualTo("SomeData");
        }

        [Fact]
        public void GIVEN_configurationIsValid_AND_execute_invoked_THEN_pitwallConfiguration_carName_is_updated()
        {
            var original = new FakeUserDefinedConfiguration()
            {
                CarName = "SomeCarName"
            };

            SetAllValidationOk();

            var target = GetTarget();

            target.Execute(original);

            Check.That(_configuration.CarName).IsEqualTo("SomeCarName");
        }

        [Fact]
        public void GIVEN_configurationIsNull_THEN_apiAddress_is_notSet()
        {
            var target = GetTarget();

            target.Execute(null);

            Check.That(_configuration.ApiAddress).IsNull();
        }

        // ---

        [Theory]
        [InlineData(true, true, true, true)]
        public void GIVEN_all_isValid_WHEN_canExecute_THEN_return_true(
            bool isPilotValid, 
            bool isPersonalKeyValid, 
            bool isApiAddressValid, 
            bool isCarNameValid)
        {
            var original = new FakeUserDefinedConfiguration()
            {
                PilotName = "data1",
                PersonalKey = "data2",
                ApiAddress = "data3",
                CarName = "data4"
            };

            _settingsValidator.IsPilotNameValid("data1")
                .Returns(isPilotValid);

            _settingsValidator.IsPersonalKeyValid("data2")
                .Returns(isPersonalKeyValid);

            _settingsValidator.IsApiAddressValid("data3")
                .Returns(isApiAddressValid);    

            _settingsValidator.IsCarNameValid("data4")
                .Returns(isCarNameValid);   

            var target = GetTarget();

            var actual = target.CanExecute(original);

            Check.That(actual).IsTrue();
        }

        [Theory]
        [InlineData(false, true, true, true)]
        [InlineData(true, false, true, true)]
        [InlineData(true, true, false, true)]
        [InlineData(true, true, true, false)]
        public void GIVEN_oneField_is_invlaid_THEN_return_false(
            bool isPilotValid,
            bool isPersonalKeyValid,
            bool isApiAddressValid,
            bool isCarNameValid)
        {
            var original = new FakeUserDefinedConfiguration()
            {
                PilotName = "data1",
                PersonalKey = "data2",
                ApiAddress = "data3",
                CarName = "data4"
            };

            _settingsValidator.IsPilotNameValid("data1")
                .Returns(isPilotValid);

            _settingsValidator.IsPersonalKeyValid("data2")
                .Returns(isPersonalKeyValid);

            _settingsValidator.IsApiAddressValid("data3")
                .Returns(isApiAddressValid);

            _settingsValidator.IsCarNameValid("data4")
                .Returns(isCarNameValid);

            var target = GetTarget();

            var actual = target.CanExecute(original);

            Check.That(actual).IsFalse();
        }

        private void SetAllValidationOk()
        {
            _settingsValidator.IsApiAddressValid(Arg.Any<string>()).Returns(true);
            _settingsValidator.IsCarNameValid(Arg.Any<string>()).Returns(true);
            _settingsValidator.IsPilotNameValid(Arg.Any<string>()).Returns(true);
            _settingsValidator.IsPersonalKeyValid(Arg.Any<string>()).Returns(true);
        }

    }
}

using NFluent;
using PitWallAcquisitionPlugin.Tests.UI.ViewModels;
using PitWallAcquisitionPlugin.UI.ViewModels;
using Xunit;

namespace PitWallAcquisitionPlugin.RunTime48.Tests.UI.Commands
{
    public class SaveToConfigurationCommandTest
    {
        private FakePitWallConfiguration _configuration;

        public SaveToConfigurationCommandTest()
        {
            _configuration = new FakePitWallConfiguration();
        }

        public ISaveToConfigurationCommand GetTarget()
        {
            return new SaveToConfigurationCommand(_configuration);
        }

        [Fact]
        public void GIVEN_parameter_is_null_THEN_canExecute_return_false()
        {
            var target = GetTarget();

            var actual = target.CanExecute(null);

            Check.That(actual).IsFalse();
        }

        [Fact]
        public void GIVEN_parameter_is_notNull_AND_param_is_definedConfiguration_THEN_return_true()
        {
            var target = GetTarget();

            var actual = target.CanExecute(new FakeUserDefinedConfiguration());

            Check.That(actual).IsTrue();
        }

        [Fact]
        public void GIVEN_configurationIsValid_AND_execute_invoked_THEN_pitwallConfiguration_pilotName_is_updated()
        {
            var original = new FakeUserDefinedConfiguration()
            {
                PilotName = "SomePilotName"
            };

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
    }
}

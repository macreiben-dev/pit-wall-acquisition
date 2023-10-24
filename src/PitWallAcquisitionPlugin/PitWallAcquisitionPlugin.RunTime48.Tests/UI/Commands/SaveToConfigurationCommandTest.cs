using NFluent;
using PitWallAcquisitionPlugin.Tests.UI.Commands;
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
    }
}

using NFluent;
using PitWallAcquisitionPlugin.UI.ViewModels;
using Xunit;

namespace PitWallAcquisitionPlugin.RunTime48.Tests.UI.Commands
{
    public class SaveToConfigurationCommandTest
    {
        public ISaveToConfigurationCommand GetTarget() { return new SaveToConfigurationCommand(); }

        [Fact]
        public void GIVEN_parameter_is_null_THEN_canExecute_return_false()
        {
            var target = GetTarget();

            var actual = target.CanExecute(null);

            Check.That(actual).IsFalse();
        }
    }
}

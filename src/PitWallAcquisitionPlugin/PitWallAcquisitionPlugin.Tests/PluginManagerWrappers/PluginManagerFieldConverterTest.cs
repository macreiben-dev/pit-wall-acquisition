using FuelAssistantMobile.DataGathering.SimhubPlugin.PluginManagerWrappers;
using NFluent;
using NSubstitute;
using PitWallAcquisitionPlugin.PluginManagerWrappers;

namespace PitWallAcquisitionPlugin.Tests.PluginManagerWrappers
{
    public class PluginManagerFieldConverterTest
    {
        private IPluginManagerAdapter _pluginManager;

        public PluginManagerFieldConverterTest()
        {
            _pluginManager = Substitute.For<IPluginManagerAdapter>();
        }

        [Fact]
        public void GIVEN_pluginManager_return_null_AND_toString_invoked_THEN_return_null()
        {
            _pluginManager.GetPropertyValue("some_key").Returns((string)null);

            var actual = PluginManagerFieldConverter.ToString("some_key", _pluginManager);

            Check.That(actual).IsNull();
        }

        [Fact]
        public void GIVEN_pluginManager_return_data_AND_toString_invoked_THEN_return_data()
        {
            _pluginManager.GetPropertyValue("some_key").Returns("data_value");

            var actual = PluginManagerFieldConverter.ToString("some_key", _pluginManager);

            Check.That(actual).IsEqualTo("data_value");
        }
    }
}

using NFluent;
using NSubstitute;
using PitWallAcquisitionPlugin.PluginManagerWrappers;
using Xunit;

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

        [Fact]
        public void GIVEN_pluginManager_return_null_AND_toDouble_invoked_THEN_return_null()
        {
            _pluginManager.GetPropertyValue("some_key").Returns((double?)null);

            var actual = PluginManagerFieldConverter.ToDouble("some_key", _pluginManager);

            Check.That(actual).IsNull();
        }

        [Fact]
        public void GIVEN_pluginManager_return_data_AND_toDouble_invoked_THEN_return_data()
        {
            _pluginManager.GetPropertyValue("some_key").Returns(23.325515);

            var actual = PluginManagerFieldConverter.ToDouble("some_key", _pluginManager);

            Check.That(actual).IsEqualTo(23.325515);
        }

        [Fact]
        public void GIVEN_pluginManager_return_null_AND_toBoolean_invoked_THEN_return_false()
        {
            _pluginManager.GetPropertyValue("some_key").Returns((bool?)null);

            var actual = PluginManagerFieldConverter.ToBoolean("some_key", _pluginManager);

            Check.That(actual).IsFalse();
        }
    }
}

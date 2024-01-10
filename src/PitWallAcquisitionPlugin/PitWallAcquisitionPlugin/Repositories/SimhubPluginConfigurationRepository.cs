using PitWallAcquisitionPlugin.UI.ViewModels;
using System;
using SimHub.Plugins;
using FuelAssistantMobile.DataGathering.SimhubPlugin.Logging;

namespace PitWallAcquisitionPlugin.Repositories
{
    internal class SimhubPluginConfigurationRepository : IPitWallConfiguration
    {
        private const string SettingsName = "PitWallAcquisitionPlugin";

        private readonly IPlugin _simhubPlugin;
        private readonly ILogger _logger;

        private InnerConfiguration _configuration;

        public SimhubPluginConfigurationRepository(
            IPlugin simhubPlugin,
            ILogger logger)
        {
            _simhubPlugin = simhubPlugin;
            _logger = logger;
        }

        public string ApiAddress
        {
            get => ReadConfiguration(c => c.ApiAddress);
            set => UpdateConfiguration((c) => c.ApiAddress = value);
        }
        public string PersonalKey
        {
            get => ReadConfiguration(c => c.PersonalKey);
            set => UpdateConfiguration((c) => c.PersonalKey = value);
        }

        public string PilotName
        {
            get => ReadConfiguration(c => c.PilotName);
            set => UpdateConfiguration((c) => c.PilotName = value);
        }
        public string CarName
        {
            get => ReadConfiguration(c => c.CarName);
            set => UpdateConfiguration((c) => c.CarName = value);
        }

        private InnerConfiguration TryToLoadConfiguration()
        {
            if (_configuration == null)
            {
                _logger.Info("Loading configuration ...");

                _configuration = _simhubPlugin.ReadCommonSettings(
                    SettingsName,
                    () => CreateNewConfiguration());

                _logger.Info("Loading configuration LOADED!");
            }

            return _configuration;
        }

        private void UpdateConfiguration(Action<InnerConfiguration> updaterFunction)
        {
            if (_configuration == null)
            {
                _logger.Info("No configuration loaded, creating a new configuration.");

                _configuration = CreateNewConfiguration();
            }

            updaterFunction(_configuration);

            _simhubPlugin.SaveCommonSettings(SettingsName, _configuration);

            _logger.Info("Plugin configuration saved.");
        }

        private string ReadConfiguration(Func<InnerConfiguration, string> selector)
        {
            var configuration = TryToLoadConfiguration();

            if (configuration == null)
            {
                _logger.Warn("Configuration is null despite trying to load it.");

                return null;
            }

            return selector(configuration);
        }

        private static InnerConfiguration CreateNewConfiguration()
        {
            return new InnerConfiguration();
        }

        private class InnerConfiguration
        {
            public string ApiAddress { get; set; }
            public string PersonalKey { get; set; }

            public string PilotName { get; set; }
            public string CarName { get; set; }
        }
    }
}

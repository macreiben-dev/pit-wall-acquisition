using PitWallAcquisitionPlugin.HealthChecks;
using PitWallAcquisitionPlugin.Tests.UI.Commands;

namespace PitWallAcquisitionPlugin.UI.ViewModels
{
    internal class PluginSettingsCommandFactory : IPluginSettingsCommandFactory
    {
        private readonly IHealthCheckService checkService;
        private readonly IPitWallConfiguration _configuration;
        private readonly ISettingsValidator _validator;

        public PluginSettingsCommandFactory(
            IHealthCheckService checkService,
            IPitWallConfiguration pitWallConfiguration, 
            ISettingsValidator validator)
        {
            this.checkService = checkService;
            _configuration = pitWallConfiguration;
            _validator = validator;
        }

        public IIsApiAvailableCommand GetInstance(
            IDisplayAvailability display)
        {
            return new IsApiAvailableCommand(display, checkService);
        }

        public ISaveToConfigurationCommand GetSaveToConfiguration()
        {
            return new SaveToConfigurationCommand(_configuration, _validator);
        }
    }
}

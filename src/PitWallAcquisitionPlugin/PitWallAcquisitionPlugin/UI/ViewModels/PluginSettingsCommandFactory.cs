using PitWallAcquisitionPlugin.HealthChecks;
using PitWallAcquisitionPlugin.Tests.UI.Commands;

namespace PitWallAcquisitionPlugin.UI.ViewModels
{
    public class PluginSettingsCommandFactory : IPluginSettingsCommandFactory
    {
        private readonly IHealthCheckService checkService;
        private readonly IPitWallConfiguration _configuration;

        public PluginSettingsCommandFactory(
            IHealthCheckService checkService, 
            IPitWallConfiguration pitWallConfiguration)
        {
            this.checkService = checkService;
            _configuration = pitWallConfiguration;
        }

        public IIsApiAvailableCommand GetInstance(
            IDisplayAvailability display)
        {
            return new IsApiAvailableCommand(display, checkService);
        }

        public ISaveToConfigurationCommand GetSaveToConfiguration()
        {
            return new SaveToConfigurationCommand(_configuration);
        }
    }
}

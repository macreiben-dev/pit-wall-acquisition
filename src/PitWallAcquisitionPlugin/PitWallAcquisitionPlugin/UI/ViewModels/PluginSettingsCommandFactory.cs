using PitWallAcquisitionPlugin.HealthChecks;
using PitWallAcquisitionPlugin.UI.Commands;

namespace PitWallAcquisitionPlugin.UI.ViewModels
{
    internal class PluginSettingsCommandFactory : IPluginSettingsCommandFactory
    {
        private readonly IHealthCheckService checkService;
        private readonly IPitWallConfiguration _configuration;
        private readonly ISettingsValidator _validator;
        private readonly ILocalWorkerFactory workerFactory;

        public PluginSettingsCommandFactory(
            IHealthCheckService checkService,
            IPitWallConfiguration pitWallConfiguration,
            ISettingsValidator validator, 
            ILocalWorkerFactory workerFactory)
        {
            this.checkService = checkService;
            _configuration = pitWallConfiguration;
            _validator = validator;
            this.workerFactory = workerFactory;
        }

        public IIsApiAvailableCommand GetInstance(
            IDisplayAvailability display)
        {
            return new IsApiAvailableCommand(display, checkService, workerFactory);
        }

        public ISaveToConfigurationCommand GetSaveToConfiguration()
        {
            return new SaveToConfigurationCommand(_configuration, _validator);
        }
    }
}

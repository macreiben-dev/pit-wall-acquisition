using PitWallAcquisitionPlugin.HealthChecks;
using PitWallAcquisitionPlugin.Tests.UI.Commands;

namespace PitWallAcquisitionPlugin.UI.ViewModels
{
    public class PluginSettingsCommandFactory : IPluginSettingsCommandFactory
    {
        private readonly IHealthCheckService checkService;

        public PluginSettingsCommandFactory(
            IHealthCheckService checkService)
        {
            this.checkService = checkService;
        }

        public IIsApiAvailableCommand GetInstance(
            IDisplayAvailability display)
        {
            return new IsApiAvailableCommand(display, checkService);
        }
    }
}

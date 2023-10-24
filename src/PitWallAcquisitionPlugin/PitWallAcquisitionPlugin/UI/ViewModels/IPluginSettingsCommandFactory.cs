using PitWallAcquisitionPlugin.Tests.UI.Commands;

namespace PitWallAcquisitionPlugin.UI.ViewModels
{
    public interface IPluginSettingsCommandFactory
    {
        IIsApiAvailableCommand GetInstance(IDisplayAvailability display);

        ISaveToConfigurationCommand GetSaveToConfiguration();
    }
}
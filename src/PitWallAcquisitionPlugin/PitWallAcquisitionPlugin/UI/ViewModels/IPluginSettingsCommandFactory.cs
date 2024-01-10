using PitWallAcquisitionPlugin.Tests.UI.Commands;

namespace PitWallAcquisitionPlugin.UI.ViewModels
{
    internal interface IPluginSettingsCommandFactory
    {
        IIsApiAvailableCommand GetInstance(IDisplayAvailability display);

        ISaveToConfigurationCommand GetSaveToConfiguration();
    }
}
using System.Windows.Input;

namespace PitWallAcquisitionPlugin.UI.ViewModels
{
    internal interface ISaveToConfigurationCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}
